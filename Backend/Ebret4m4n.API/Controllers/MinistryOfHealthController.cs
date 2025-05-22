using Ebret4m4n.Shared.DTOs.AdminsDto.MinistryOfHealthAdminDtos;
using Ebret4m4n.Shared.DTOs.AdminsDto.GovernorateAdminDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ebret4m4n.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using Ebret4m4n.Entities.Models;
using Ebret4m4n.Contracts;
using Mapster;



namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "admin")]
[ApiController]
public class MinistryOfHealthController
    (IUnitOfWork unitOfWork,
    UserManager<ApplicationUser> userManager) : ControllerBase
{
    [HttpGet("governorates")]
    public IActionResult GetGovernorates()
    {
        var governorates = unitOfWork.GovernorateAdminRepo.FindAll(false)
            .Select(gov => gov.Governorate)
            .ToList();

        var response = GeneralResponse<List<string>>.SuccessResponse(governorates);

        return Ok(response);
    }

    [HttpGet("governorate-details")]
    public async Task<IActionResult> GovernorateDetails([FromQuery] string governorateName)
    {
        var governorate =
            await unitOfWork.GovernorateAdminRepo.FindAsync(gov => gov.Governorate == governorateName, false, ["User", "MainInventories", "CityAdminStaffs.HealthCareCenters"]);

        var governorateDetails = governorate.Adapt<GovernorateDetailsDto>();

        var response = GeneralResponse<GovernorateDetailsDto>.SuccessResponse(governorateDetails);

        return Ok(response);
    }

    [HttpGet("{cityAdminId}/cities-healthcares-datails")]
    public async Task<IActionResult> GetCityHealthCareDetails([FromRoute] string cityAdminId)
    {
        var healthcaresCityAdmins =
            await unitOfWork.CityAdminStaffRepo.FindAsync(c => c.UserId == cityAdminId, false, ["MainInventories", "User", "HealthCareCenters"]);
            
        if(healthcaresCityAdmins is null)
            return NotFound(GeneralResponse<string>.FailureResponse("لا يوجد ادمن لهذه المدينه"));

        var healthcaresCityAdminsDto = healthcaresCityAdmins.Adapt<HealthCaresCityDto>();

        var response = GeneralResponse<HealthCaresCityDto>.SuccessResponse(healthcaresCityAdminsDto);

        return Ok(response);   
    }

    [HttpPost("add-governorate-admin")]
    public async Task<IActionResult> AddGovernorateAdmin([FromBody] AddGovernorateAdminDto model)
    {
        if(!ModelState.IsValid)
            return UnprocessableEntity(
                GeneralResponse<string>.FailureResponse("تاكد من ان جميع البيانات المدخله صحيحه"));


        await unitOfWork.BeginTransactionAsync();

        try
        {
            var user = model.Adapt<ApplicationUser>();

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest("الرجاء التاكد من البريد الالكتروني او هذا الايميل موجود بالفعل");

            var roleResult = await userManager.AddToRoleAsync(user, "governorateAdmin");

            if(!roleResult.Succeeded)
                return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم اضافه الادمن للدور"));

            var governorateAdmin = new GovernorateAdminStaff() { Governorate = model.Governorate, UserId = user.Id };

            await unitOfWork.GovernorateAdminRepo.AddAsync(governorateAdmin);

            var dbResult = await unitOfWork.SaveAsync();

            if (dbResult == 0)
                return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم حفظ البيانات حاول مره اخري"));

            var response = GeneralResponse<string>.SuccessResponse("تم اضافه هذا الادمن بنجاح");

            await unitOfWork.CommitTransactionAsync();

            return Ok(response);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            return StatusCode(StatusCodes.Status500InternalServerError, GeneralResponse<string>.FailureResponse($"{ex.Message} :حدث خطا ما اثناء تسجيل البينات"));
        }
    }

    [HttpGet("governorate-admins")]
    public IActionResult GovernorateAdmins()
    {
        var admins = unitOfWork.GovernorateAdminRepo.FindAll(false, ["User"])
            .Select(admin => admin.Adapt<GovernorateAdminsDto>())
            .ToList() ?? [];

        var response = GeneralResponse<List<GovernorateAdminsDto>>.SuccessResponse(admins);

        return Ok(response);
    }

    [HttpGet("{governorateAdminId:guid}/governorate-admin-details")]
    public async Task<IActionResult> GovernorateAdminDetails(Guid governorateAdminId)
    {
        var admin = await unitOfWork.GovernorateAdminRepo.FindAsync(admin => admin.UserId == governorateAdminId.ToString(), false, ["User"]);

        if (admin is null)
            return NotFound(GeneralResponse<string>.FailureResponse("لا يوجد ادمن بهذا الرقم"));

        var adminDetails = admin.Adapt<GovernorateAdminsDto>();

        var response = GeneralResponse<GovernorateAdminsDto>.SuccessResponse(adminDetails);

        return Ok(response);
    }

    [HttpPut("{governorateAdminId:guid}/update-governorate-admins")]
    public async Task<IActionResult> UpdateGovernorateAdmin(Guid governorateAdminId, [FromBody] UpdateGovernorateAdminDto model)
    {
        var admin  = await unitOfWork.GovernorateAdminRepo.FindAsync(admin => admin.UserId == governorateAdminId.ToString(), true, ["User"]);

        if (admin is null)
            return NotFound(GeneralResponse<string>.FailureResponse("لا يوجد ادمن بهذا الرقم"));

        admin.User.NormalizedEmail = userManager.NormalizeEmail(model.Email);
        model.Adapt(admin.User);

        var result = await userManager.UpdateAsync(admin.User);

        if (!result.Succeeded)
            return BadRequest(GeneralResponse<string>.FailureResponse("الرجاء التاكد من ان البريد الالكتروني صحيح او هذا الايميل موجود بالفعل"));

        admin.Governorate = model.Governorate;

        unitOfWork.GovernorateAdminRepo.Update(admin);

        var dbResult = await unitOfWork.SaveAsync();

        if (dbResult == 0)
            return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم حفظ البيانات حاول مره اخري"));

        var response = GeneralResponse<string>.SuccessResponse("تم تعديل البيانات بنجاح");
        return Ok(response);
    }
}