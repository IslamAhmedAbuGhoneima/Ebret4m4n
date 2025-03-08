using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Exceptions;
using Ebret4m4n.Shared.DTOs;
using Ebret4m4n.Shared.DTOs.ComplaintDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mapster;
using Ebret4m4n.Entities.Models;
using Microsoft.AspNetCore.Identity;


namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "cityAdmin")]
[ApiController]
public class CityAdminController
    (IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager) : ControllerBase
{

    [HttpGet("healthcareCenter-village")]
    public async Task<IActionResult> GetHealthCareInVillageAsync()
    {
        var city = User.FindFirst("city")!.Value;

        var healthCareCenters = await unitOfWork.HealthCareCenterRepo
            .FindByCondition(hc => hc.City == city, false)
            .Select(hc => hc.Village)
            .ToListAsync();

        return Ok(healthCareCenters);
    }

    [HttpGet("{id:guid}/healthCareCenter")]
    public async Task<IActionResult> HealthCareDetails(Guid id)
    {
        
        var healthCareCenter = await unitOfWork.HealthCareCenterRepo
            .FindAsync(hc => hc.HealthCareCenterId == id, false);

        if (healthCareCenter is null)
            return NotFound();
        // oraginzer info
        //الاسم - email-city-position

        //var organizer = unitOfWork.MedicalApplicationRepo
        //    .FindByCondition(m => m.HealthCareName == healthCareCenter.HealthCareCenterName
        //   && m.ApplicantPosition == ApplicantPosition.Organizer, false).FirstOrDefault();


        //Doctor info

        //var doctor = unitOfWork.MedicalApplicationRepo
        //    .FindByCondition(m => m.HealthCareName == healthCareCenter.HealthCareCenterName
        //    && m.ApplicantPosition == ApplicantPosition.Doctor, false).FirstOrDefault();

        // inventory info

        //var inventory = UnitOfWork.InventoryRepo
        //    .FindByCondition(i => i.HealthCareCenterId == id, false).FirstOrDefault();
        return Ok();
    }


    [HttpPost("medical-postion-add")]
    public async Task<IActionResult> AddMedicalPostion(MedicalStaffDto model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var healthCare =
            await unitOfWork.HealthCareCenterRepo.FindAsync(hc => hc.HealthCareCenterId == model.HealthCareCenterId, false);

        if (healthCare is null)
            throw new BadRequestException("لم يتم العثور علي هذه الوحده الصحيه");

        var user = model.Adapt<ApplicationUser>();

        var result = await userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            throw new BadRequestException("لم يتم انشاء هذا المستخدم");

        await userManager.AddToRoleAsync(user, model.StaffRole);

        var medicalStaff = (model, healthCare).Adapt<MedicalStaff>();

        medicalStaff.UserId = user.Id;
            
        await unitOfWork.StaffRepo.AddAsync(medicalStaff);
        var dbResult = await unitOfWork.SaveAsync();

        if (dbResult == 0)
            throw new BadRequestException("لم يتم حفظ الباينات اللرجاء المحاوله مره اخري");

        var response = new GeneralResponse<string>(StatusCodes.Status200OK, "تم اضافه هذا المستخدم بنجاح");

        return Ok(response);
    }

    [HttpGet("complaints")]
    public async Task<IActionResult> Complaints()
    {
        var city = User.FindFirst("city")!.Value;

        var complaints = await unitOfWork.ComplaintRepo
            .FindByCondition(c => c.User.City == city, false, "User")
            .Select(C => new ComplaintsDto(C.User.UserName, C.DateSubmitted))
            .ToListAsync();
            

        if (complaints is null)
            throw new NotFoundBadRequest("لا توجد شكاوي مسجله");

        var response = new GeneralResponse<List<ComplaintsDto>> (StatusCodes.Status200OK, complaints);

        return Ok(response);
    }

    [HttpGet("{id:guid}/complaint")]
    public async Task<IActionResult> Complaint(Guid id)
    {
        var complaint =
            await unitOfWork.ComplaintRepo.FindAsync(c => c.Id == id, false, "User");

        if (complaint is null)
            throw new NotFoundBadRequest("لا توجد شكاوي مسجله لهذا المستخدم");

        var healthCare = 
            await unitOfWork.HealthCareCenterRepo.FindAsync(hc => hc.HealthCareCenterId == complaint.User.HealthCareCenterId, false);

        var complaintDto = (complaint, healthCare).Adapt<ComplaintDto>();

        var response = new GeneralResponse<ComplaintDto>(StatusCodes.Status200OK, complaintDto);

        return Ok(response);
    }

    [HttpPost("{complaintId:guid}/handle-complaint")]
    public async Task<IActionResult> HandleComplaint(Guid complaintId)
    {
        var complaint =
            await unitOfWork.ComplaintRepo.FindAsync(C => C.Id == complaintId, false, "User");

        if (complaint is null)
            throw new NotFoundException("هذه الشكوي غير موجوده او تم حذفها");

        // send email or notfication to user

        return Ok();
    }

    [HttpPost("{vaccineId:guid}/send-vaccine")]
    public IActionResult SendVaccine(Guid vaccineId , int amount)
    {
        return Ok();
    } 
}
