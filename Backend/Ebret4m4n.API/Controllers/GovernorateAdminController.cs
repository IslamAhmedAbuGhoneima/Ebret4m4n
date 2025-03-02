using Ebret4m4n.Shared.DTOs.AdminsDto.CityAdminDots;
using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.Entities.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ebret4m4n.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ebret4m4n.Contracts;
using Mapster;
using Ebret4m4n.Shared.DTOs.JobApplicationsDtos;

namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles ="governorateAdmin")]
[ApiController]
public class GovernorateAdminController
    (IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager): ControllerBase
{
    [HttpGet("cities")]
    public async Task<IActionResult> GetCities()
    {
        var adminId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var admin =
           await unitOfWork.GovernorateAdminRepo.FindAsync(G => G.UserId == adminId, false);

        var cities =
            await unitOfWork.HealthCareCenterRepo
            .FindByCondition(hc => hc.Governorate == admin.Governorate, false)
            .Select(hc => hc.City)
            .Distinct()
            .ToListAsync();
            
        return Ok(cities);
    }

    [HttpGet("city-details")]
    public async Task<IActionResult> CityInfo([FromQuery]string cityName)
    {
        var cityAdminId =
             (await unitOfWork.CityAdminStaffRepository.FindAsync(C => C.City == cityName, false)).UserId;

        if (cityAdminId is null)
            throw new NotFoundBadRequest("لا يوجد ادمن لهذه المدينه");

        var cityAdmin = await userManager.FindByIdAsync(cityAdminId);

        if (cityAdmin is null)
            throw new BadRequestException("لا يوجد مدير لهذه المدينه الرجاء اضافه مدير");

        // edit when inventory created
        var cityDetails = cityAdmin.Adapt<CityRecordDetailsDto>();

        return Ok(cityDetails);
    }


    [HttpGet("{position:alpha}/applied-postions")]
    public IActionResult GetAppliedPostions(string position)
    {
        if (Enum.TryParse(position, out ApplicantPosition pos) == false)
            throw new BadRequestException("لم يتم العثور علي نتيجه");

        var governorateAdmin = User.FindFirst("governorate")!.Value;

        var positions = unitOfWork.MedicalApplicationRepo
                            .FindByCondition(j => j.ApplicantPosition == pos && j.HealthCareGovernorate == governorateAdmin, false, ["User"]);


        if (positions == null)
            throw new NotFoundException("لا يوجد تقديمات لهذا المنصب");

        var postionsDto = positions.Adapt<List<MedicalPositionRequestsDto>>();

        return Ok(postionsDto);

    }

    [HttpPost("{id:guid}/approve-postion")]
    public async Task<IActionResult> ApprovePostion(Guid id)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var governorateAdmin = User.FindFirst("governorate")!.Value;


        var jobPostion = await unitOfWork.MedicalApplicationRepo.FindAsync(p => p.ApplicationId == id, true);

        if (jobPostion == null)
            throw new NotFoundBadRequest("هذه البيانات غير موجوده او لم يتم تسجيل احد بها");

        var healthCare = await unitOfWork.HealthCareCenterRepo.FindAsync(hc => hc.HealthCareCenterName == jobPostion.HealthCareName, false);

        if (healthCare == null)
            throw new NotFoundBadRequest("لا يوجد وحده صحيه بهذا الاسم");

        jobPostion.ApplicationStatus = ApplicationStatus.Accepted;
        unitOfWork.MedicalApplicationRepo.Update(jobPostion);

        var user = await userManager.FindByIdAsync(jobPostion.UserId);
        if (user == null)
            throw new NotFoundBadRequest("هذا الحساب غير موجود او تم حذف الحساب لهذاالشخص");

        var medicalNumber = jobPostion.MedicalNumber;


        var staffRecord = (user, healthCare, medicalNumber).Adapt<MedicalStaff>();


        staffRecord.StaffRole = jobPostion.ApplicantPosition.ToString();

        await unitOfWork.StaffRepository.AddAsync(staffRecord);

        var result = await unitOfWork.SaveAsync();
        var roleResult = await userManager.AddToRoleAsync(user, staffRecord.StaffRole);


        if (result == 0 || !roleResult.Succeeded)
            throw new BadRequestException("لم يتم اسناد الدور للمستخدم حاول مره اخري");


        // Send Notification message

        return Ok(new { Meessage = $"{user.UserName}تم اسناد الوضيف للمستخدم" });
    }

    [HttpPost("{id:guid}/reject-postion")]
    public async Task<IActionResult> RejectPostion(Guid id)
    {
        var jobApplication =
            await unitOfWork.MedicalApplicationRepo.FindAsync(job => job.ApplicationId == id, false);

        if (jobApplication == null)
            throw new BadRequestException("هذا الطلب غير موجود");

        jobApplication.ApplicationStatus = ApplicationStatus.Rejected;
        unitOfWork.MedicalApplicationRepo.Update(jobApplication);

        var result = await unitOfWork.SaveAsync();
        if (result == 0)
            throw new BadRequestException("حدث خطا اثناء حفظ البيانات الرجاء المحاوله مره اخري");

        return Ok(new { Message = "تم رفض الطلب الخاص بهذا المستخدم" });
    }


    //[HttpPost("vaccine-send")]
    //public IActionResult SendVaccines(SendVaccinRequestDto model)
    //{
    //    if (!ModelState.IsValid)
    //        return UnprocessableEntity(ModelState);
    //    return Ok();
    //}


}
