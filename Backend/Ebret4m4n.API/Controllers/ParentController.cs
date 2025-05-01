using Ebret4m4n.Shared.DTOs.ComplaintDtos;
using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.Shared.DTOs.ParentDtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Ebret4m4n.Entities.Exceptions;
using Ebret4m4n.Entities.Models;
using Microsoft.AspNetCore.Mvc;
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
        var appointment = await unitOfWork.AppointmentRepo.FindAsync(app => app.Id == id, false);

        if (appointment is null)
            throw new NotFoundBadRequest("لا يوجد حجز بهذا المعاد");

        var appointmentDto = appointment.Adapt<AppointmentDto>();

        var response = GeneralResponse<AppointmentDto>.SuccessResponse(appointmentDto);

        return Ok(response);
    }

    [HttpPost("appointment-book")]
    public async Task<IActionResult> AppointmentBook([FromBody] AddAppointmentDto model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(GeneralResponse<string>.FailureResponse("تاكد ان جميع مدخلات الحجز صحيحه"));

        if (model.Date <= DateTime.UtcNow)
            throw new BadRequestException("لا يمكن حجز هذ الموعد");

        var appointmentExists =
            await unitOfWork.AppointmentRepo.ExistsAsync(appointment => appointment.VaccineName == model.VaccineName && appointment.ChildId == model.ChildId);

        if (appointmentExists)
            throw new BadRequestException("تم حجز معاد لهذا اللقاح من قبل");

        var parentId = User.FindFirst("id")!.Value;

        var user = await userManager.Users
            .Include(U => U.HealthCareCenter)
            .Include(U => U.Children.Where(c => c.Id == model.ChildId && c.UserId == parentId))
            .FirstOrDefaultAsync(U => U.Id == parentId);


        if (user?.Children is null)
            throw new BadRequestException($"you do not have children with {model.ChildId} to reserve an appointment");

        if (user?.HealthCareCenterId == null)
            throw new NotFoundBadRequest("هذا المستخدم لا ينتمي الي اي وحده صحيه");

        var appointment = (model, user.HealthCareCenter, parentId).Adapt<Appointment>();

        await unitOfWork.AppointmentRepo.AddAsync(appointment);
        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            throw new BadRequestException("لم يتم حفظ الحجز الخاص بك الرجاء المحاوله مره اخري");


        string childName = user.Children.FirstOrDefault()!.Name;

        var appointmentDto = (appointment, childName).Adapt<AppointmentDto>();

        var response = GeneralResponse<AppointmentDto>.SuccessResponse(appointmentDto);

        return Ok(response);
    }

    [HttpPut("{childId}/appointment-reschedule")]
    public async Task<IActionResult> RescheduleAppointment([FromRoute] string childId, AppointmentRescheduleDto model)
    {
        var parentId = User.FindFirst("id")!.Value;

        if (model.Date <= DateTime.UtcNow)
            throw new BadRequestException("لا يمكن اضافه هذا المعاد");

        var appointment = 
            await unitOfWork.AppointmentRepo.FindAsync(appointment => appointment.ChildId == childId && appointment.UserId == parentId, true);

        if (appointment is null)
            throw new NotFoundBadRequest("لم يتم ايجاد اي حجز لهذا الطفل");

        appointment.Date = model.Date;

        unitOfWork.AppointmentRepo.Update(appointment);

        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            throw new BadRequestException("لم يتم اعاده جدوله هذا الحجز");

        var response = GeneralResponse<string>.SuccessResponse("تم اعاده الجدوله بنجاح");

        return StatusCode(StatusCodes.Status204NoContent, response);
    }

    [HttpDelete("{id:guid}/appointment-cancle")]
    public async Task<IActionResult> AppointmentCancle(Guid id)
    {
        var appointment = await unitOfWork.AppointmentRepo.FindAsync(a => a.Id == id, false);

        if (appointment == null)
            throw new NotFoundBadRequest("لا يوجد موعد خاص بهذا المستخدم");

        unitOfWork.AppointmentRepo.Remove(appointment);
        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            throw new BadRequestException("لم يتم الغاء المعاد حاول مره اخري");

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
            throw new BadRequestException("لم يتم ارسال الشكوي الخاص بك الرجاء المحاوله مره اخري");

        var response = GeneralResponse<string>.SuccessResponse("شكرا لتعونك معنا سيتم حل الشكوي الخاص بك في اقرب وقت");

        return Ok(response);
    }
}
