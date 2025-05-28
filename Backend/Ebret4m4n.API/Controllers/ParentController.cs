using Ebret4m4n.Shared.DTOs.AuthenticationDtos;
using Ebret4m4n.Shared.DTOs.HealthCareDtos;
using Ebret4m4n.Shared.DTOs.ComplaintDtos;
using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.Shared.DTOs.ParentDtos;
using Ebret4m4n.Shared.DTOs.ChildDtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Ebret4m4n.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Ebret4m4n.API.Utilites;
using Ebret4m4n.Shared.DTOs;
using Ebret4m4n.Contracts;
using Mapster;




namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "parent")]
[ApiController]
public class ParentController
    (IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : ControllerBase
{

    [HttpGet("healthcare-details")]
    public async Task<IActionResult> HealthCareDetails()
    {
        var parentId = User.FindFirst("id")!.Value;

        var user = await userManager.Users
            .Include(U => U.HealthCareCenter)
            .FirstOrDefaultAsync(U => U.Id == parentId);

        if (user?.HealthCareCenter is null)
            return NotFound(GeneralResponse<string>.FailureResponse("هذا المستخدم لا ينتمي الي اي وحده صحيه"));

        var healthCareDetailsDto = user.HealthCareCenter.Adapt<HealthCareDetailsDto>();

        var response = GeneralResponse<HealthCareDetailsDto>.SuccessResponse(healthCareDetailsDto);

        return Ok(response);
    }

    [HttpGet("get-healthcare-doctor-id")]
    public async Task<IActionResult> HealthCareDoctor()
    {
        var parentId = User.FindFirst("id")!.Value;

        var parent = userManager.Users.Include(parent => parent.HealthCareCenter)
            .FirstOrDefault(parent => parent.Id == parentId);


        if (parent is null)
            return NotFound(GeneralResponse<string>.FailureResponse("لم يتم العثور على بيانات المستخدم"));

        var doctor = await unitOfWork.StaffRepo.FindAsync(staff => staff.HCCenterId == parent.HealthCareCenterId &&
        staff.StaffRole == StaffRole.Doctor, false);
          

        if (doctor is null)
            return NotFound(GeneralResponse<string>.FailureResponse("لم يتم العثور على طبيب في هذه الوحدة الصحية"));

        var response = GeneralResponse<object>.SuccessResponse(new { DoctorId = doctor.UserId });

        return Ok(response);
    }

    [HttpGet("children-reservations")]
    public IActionResult ParentReservations()
    {
        var userId = User.FindFirst("id")!.Value;

        var appointments =
            unitOfWork.AppointmentRepo.FindByCondition(
                a => a.UserId == userId && a.Date >= DateTime.UtcNow.Date, false, ["Child"])
            .ToList() ?? [];

        var userReservationsDto = appointments.Adapt<List<UserReservationDto>>();

        var response = GeneralResponse<List<UserReservationDto>>.SuccessResponse(userReservationsDto);

        return Ok(response);
    }

    [HttpGet("{id:guid}/appointment-details")]
    public async Task<IActionResult> AppointmentDetails(Guid id)
    {
        var appointment = await unitOfWork.AppointmentRepo.FindAsync(app => app.Id == id, false, ["Child"]);

        if (appointment is null)
            return NotFound(GeneralResponse<string>.FailureResponse("لم يتم العثور علي اي حجز"));

        var appointmentDto = (appointment,appointment.Child.Name).Adapt<AppointmentDto>();

        var response = GeneralResponse<AppointmentDto>.SuccessResponse(appointmentDto);

        return Ok(response);
    }

    [HttpGet("{childId}/{vaccineName}/appointment-exists")]
    public async Task<IActionResult> AppointmentExists(string childId, string vaccineName)
    {
        var appointment = await unitOfWork.AppointmentRepo.FindAsync(a => a.ChildId == childId && a.VaccineName == vaccineName,false);

        if (appointment is null)
            return NotFound(GeneralResponse<object>.FailureResponse(new { IsReserved = false }));

        var response = GeneralResponse<object>.SuccessResponse(new { IsReserved = true, AppointmentId = appointment.Id });

        return Ok(response);
    }

    [HttpPost("appointment-book")]
    public async Task<IActionResult> AppointmentBook([FromBody] AddAppointmentDto model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(GeneralResponse<string>.FailureResponse("تاكد ان جميع مدخلات الحجز صحيحه"));

        try
        {
            var date = Utility.GetDateOfNextDay(model.Day);

            if (date <= DateTime.UtcNow)
                return BadRequest(GeneralResponse<string>.FailureResponse("لا يمكن حجز هذ الموعد"));

            var appointmentExists =
                await unitOfWork.AppointmentRepo.ExistsAsync(appointment => appointment.VaccineName == model.VaccineName && appointment.ChildId == model.ChildId);

            if (appointmentExists)
                return BadRequest(GeneralResponse<string>.FailureResponse("تم حجز معاد لهذا اللقاح من قبل"));

            var parentId = User.FindFirst("id")!.Value;

            var user = await userManager.Users
                .Include(U => U.HealthCareCenter)
                .Include(U => U.Children.Where(c => c.Id == model.ChildId && c.UserId == parentId))
                .FirstOrDefaultAsync(U => U.Id == parentId);


            if (user?.Children is null)
                return BadRequest(GeneralResponse<string>.FailureResponse("ليس لديك اي اطفال لتتمكن من حجز تطعيم اضف طفل اولا"));

            if (user?.HealthCareCenterId == null)
                return NotFound(GeneralResponse<string>.FailureResponse("هذا المستخدم لا ينتمي الي اي وحده صحيه"));

            var appointment = (model, user.HealthCareCenter, parentId).Adapt<Appointment>();
            appointment.Date = date;

            await unitOfWork.AppointmentRepo.AddAsync(appointment);
            var result = await unitOfWork.SaveAsync();

            if (result == 0)
                return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم حفظ الحجز الخاص بك الرجاء المحاوله مره اخري"));


            string childName = user.Children.FirstOrDefault()!.Name;

            var appointmentDto = (appointment, childName).Adapt<AppointmentDto>();

            var response = GeneralResponse<AppointmentDto>.SuccessResponse(appointmentDto);

            return Ok(response);
        }
        catch(Exception ex)
        {
            return BadRequest(GeneralResponse<string>.FailureResponse(ex.Message));
        }
        
    }

    [HttpPut("{appointmentId:guid}/appointment-reschedule")]
    public async Task<IActionResult> AppointmentReschedule([FromRoute] Guid appointmentId, AppointmentRescheduleDto model)
    {
        try
        {
            var date = Utility.GetDateOfNextDay(model.Day);

            if (date <= DateTime.UtcNow)
                return BadRequest(GeneralResponse<string>.FailureResponse("لا يمكن اضافه هذا المعاد"));

            var appointment =
                await unitOfWork.AppointmentRepo.FindAsync(appointment => appointment.Id == appointmentId, true);

            if (appointment is null)
                return NotFound(GeneralResponse<string>.FailureResponse("لم يتم ايجاد اي حجز لهذا الطفل"));

            appointment.Date = date;

            unitOfWork.AppointmentRepo.Update(appointment);

            var result = await unitOfWork.SaveAsync();

            if (result == 0)
                return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم اعاده جدوله هذا الحجز"));

            var response = GeneralResponse<string>.SuccessResponse("تم اعاده الجدوله بنجاح");

            return StatusCode(StatusCodes.Status204NoContent, response);
        }
        catch (Exception ex)
        {
            return BadRequest(GeneralResponse<string>.FailureResponse(ex.Message));
        }
        
    }

    [HttpDelete("{id:guid}/appointment-cancle")]
    public async Task<IActionResult> AppointmentCancle(Guid id)
    {
        var appointment = await unitOfWork.AppointmentRepo.FindAsync(a => a.Id == id, false);

        if (appointment == null)
            return NotFound(GeneralResponse<string>.FailureResponse("لا يوجد موعد خاص بهذا المستخدم"));

        unitOfWork.AppointmentRepo.Remove(appointment);
        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم الغاء المعاد حاول مره اخري"));

        return NoContent();
    }

    [HttpPost("complaint-submit")]
    public async Task<IActionResult> SubmitComplaint(ComplaintMessageDto model)
    {
        var parentId = User.FindFirst("id")!.Value;

        var complaint = model.Adapt<Complaint>();
        complaint.UserId = parentId;

        await unitOfWork.ComplaintRepo.AddAsync(complaint);

        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم ارسال الشكوي الخاص بك الرجاء المحاوله مره اخري"));

        var response = GeneralResponse<string>.SuccessResponse("شكرا لتعونك معنا سيتم حل الشكوي الخاص بك في اقرب وقت");

        return Ok(response);
    }

    [HttpGet("{id:guid}/user-profile")]
    public async Task<IActionResult> UserProfile(Guid id)
    {
        string userId = id.ToString();
        ApplicationUser? _user = await userManager.Users
            .Include(U => U.Children.Where(C => C.UserId == userId))
            .FirstOrDefaultAsync(U => U.Id == userId);

        if (_user is null)
            return NotFound(GeneralResponse<string>.FailureResponse($"لا يوجد مستخدم بهذا الرقم : {_user.Id}"));


        var userChildren = new List<ChildDto>();
        foreach (var child in _user.Children)
        {
            var childDto = child.Adapt<ChildDto>();
            userChildren.Add(childDto);
        }

        var userDto = _user.Adapt<UserDataDto>();

        var response = GeneralResponse<UserDataDto>.SuccessResponse(userDto);

        return Ok(response);
    }

    [HttpPut("update-parent")]
    public async Task<IActionResult> UpdateParent([FromBody] UpdateParentDto model)
    {
        var parentId = User.FindFirst("id")!.Value;

        var parent = await userManager.FindByIdAsync(parentId);

        if (parent is null)
            return NotFound(GeneralResponse<string>.FailureResponse("لا يوجد مستخدم بهذا الرقم"));

        var healthCareCenter = await unitOfWork.HealthCareCenterRepo
            .ExistsAsync(h => h.HealthCareCenterId == model.HealthCareCenterId);

        if(!healthCareCenter)
            return NotFound(GeneralResponse<string>.FailureResponse("لا يوجد وحده صحيه بهذا الرقم"));

        model.Adapt(parent);

        var result = await userManager.UpdateAsync(parent);

        if(!result.Succeeded)
            return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم تحديث بياناتك الرجاء التاكد من ادخال بيانات صحيحه"));

        var response = GeneralResponse<string>.SuccessResponse("تم تحديث بياناتك بنجاح");

        return Ok(response);
    }
}
