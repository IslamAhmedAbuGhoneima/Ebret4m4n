﻿using Ebret4m4n.Shared.DTOs.AdminsDto.CityAdminDots;
using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.Entities.Exceptions;
using Microsoft.AspNetCore.Identity;                
using Ebret4m4n.Entities.Models;                    
using Microsoft.AspNetCore.Mvc;                     
using System.Security.Claims;                                              
using Ebret4m4n.Shared.DTOs;                        
using Ebret4m4n.Contracts;                          
using Mapster;


namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "governorateAdmin,admin")]
[ApiController]
public class GovernorateAdminController
    (IUnitOfWork unitOfWork, 
    UserManager<ApplicationUser> userManager) : ControllerBase
{
    [HttpGet("cities")]
    public IActionResult GetCities([FromQuery] string? governorate)
    {
        string adminGovernorate = string.Empty;
        var adminRole = User.FindFirst(ClaimTypes.Role)!.Value;

        if (adminRole == "governorateAdmin")
            adminGovernorate = User.FindFirst("governorate")!.Value;

        if (adminRole == "admin" && governorate == null)
            throw new BadRequestException("من فظلك ادخل اسم المحافظه لتحميل المراكز الخاصه بها");

        var targetGovernorate = adminRole == "admin" ? governorate : adminGovernorate;

        if (governorate is not null && adminGovernorate != governorate && adminRole != "admin")
            throw new BadRequestException("لا يمكنك عرض مدن محافظه اخري");

        
        var cities = unitOfWork.HealthCareCenterRepo
            .FindByCondition(hc => hc.Governorate == targetGovernorate, false)
            .Select(hc => hc.City)
            .Distinct()
            .ToList();

        var response = new GeneralResponse<List<string>>(StatusCodes.Status200OK, cities);

        return Ok(response);
    }
    [HttpGet("city-details")]
    
    public async Task<IActionResult> CityDetails([FromQuery] string cityName)
    {
        var cityAdminStaff =
             await unitOfWork.CityAdminStaffRepo.FindAsync(C => C.City == cityName, false, ["MainInventories", "User"]);

        if (cityAdminStaff is null)
            throw new NotFoundBadRequest("لا يوجد ادمن لهذه المدينه");


        var cityDetails = cityAdminStaff.Adapt<CityRecordDetailsDto>();

        var response = new GeneralResponse<CityRecordDetailsDto>(StatusCodes.Status200OK, cityDetails);

        return Ok(response);
    }

    [HttpGet("city-admins")]
    [Authorize(Roles = "governorateAdmin")]
    public IActionResult CityAdmins()
    {
        var governorateAdminId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var cityAdmins =
            unitOfWork.CityAdminStaffRepo.FindByCondition(admin => admin.GovernorateAdminId == governorateAdminId, false, "User")
            .Select(admin => admin.Adapt<CityAdminsDto>())
            .ToList() ?? [];

        var response = new GeneralResponse<List<CityAdminsDto>>(StatusCodes.Status200OK, cityAdmins);

        return Ok(response);
    }

    [HttpPost("city-admin-add")]
    [Authorize(Roles = "governorateAdmin")]
    public async Task<IActionResult> AddCityAdmin(AddCityAdminDto model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(new GeneralResponse<string>(StatusCodes.Status422UnprocessableEntity,"تاكد ان جميع المدخلات صحيحه"));

        var checkAdminCityExists = await unitOfWork.CityAdminStaffRepo.ExistsAsync(city => city.City == model.City);

        if (checkAdminCityExists)
            throw new BadRequestException("يوجد ادمن لهذه المدينه");

        var governorateAdminId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var user = model.Adapt<ApplicationUser>();

        var result = await userManager.CreateAsync(user, model.Password);

        if(!result.Succeeded)
            throw new BadRequestException("لم يتم انشاء هذا المستخدم");

        await userManager.AddToRoleAsync(user, model.Role);
        
        var cityAdmin = model.Adapt<CityAdminStaff>();
        cityAdmin.UserId = user.Id;
        cityAdmin.GovernorateAdminId = governorateAdminId;

        await unitOfWork.CityAdminStaffRepo.AddAsync(cityAdmin);
        var dbResult = await unitOfWork.SaveAsync();

        if (dbResult == 0)
            throw new BadRequestException("لم يتم حفظ البيانات الخاصه بهذا المستخدم");

        var response = new GeneralResponse<string>(StatusCodes.Status200OK, "تم اضافه ادمن مدينه بنجاح");

        return Ok(response);
    }

    [HttpDelete("{cityAdminId:guid}/remove-admin")]
    [Authorize(Roles = "governorateAdmin")]
    public async Task<IActionResult> DeleteCityAdmin(Guid cityAdminId)
    {
        var adminId = cityAdminId.ToString();

        var cityAdmin =
            await unitOfWork.CityAdminStaffRepo.FindAsync(admin => admin.UserId == adminId, false);

        if (cityAdmin == null)
            throw new NotFoundException("لم يتم العثور علي هذا المستخدم");

        var user = await userManager.FindByIdAsync(adminId);

        unitOfWork.CityAdminStaffRepo.Remove(cityAdmin);

        await userManager.RemoveFromRoleAsync(user!, "cityAdmin");

        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            throw new BadRequestException("لم يتم حذف هذا المستخدم حاول مره اخري");

        var response = new GeneralResponse<string>(StatusCodes.Status204NoContent, "تم حذف هذا المستخدم");

        return Ok(response);
    }
}