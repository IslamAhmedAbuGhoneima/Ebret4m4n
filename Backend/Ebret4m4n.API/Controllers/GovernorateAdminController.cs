using Ebret4m4n.Shared.DTOs.AdminsDto.CityAdminDots;
using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.Shared.DTOs.OrderDtos;
using Ebret4m4n.Entities.Exceptions;
using Microsoft.AspNetCore.Identity;                
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Ebret4m4n.Entities.Models;                    
using Microsoft.AspNetCore.Mvc;                     
using System.Security.Claims;                       
using Ebret4m4n.API.Utilites;                       
using Ebret4m4n.Shared.DTOs;                        
using Ebret4m4n.Contracts;                          
using Ebret4m4n.API.Hubs;
using Mapster;

namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "governorateAdmin")]
[ApiController]
public class GovernorateAdminController
    (IUnitOfWork unitOfWork, 
    UserManager<ApplicationUser> userManager) : ControllerBase
{
    [HttpGet("cities")]
    public async Task<IActionResult> GetCities()
    {
        var adminGovernorate = User.FindFirst("governorate")!.Value;

        var cities =
            await unitOfWork.HealthCareCenterRepo
            .FindByCondition(hc => hc.Governorate == adminGovernorate, false)
            .Select(hc => hc.City)
            .Distinct()
            .ToListAsync();

        var response = new GeneralResponse<List<string>>(StatusCodes.Status200OK, cities);

        return Ok(response);
    }

    [HttpGet("city-details")]
    public async Task<IActionResult> CityInfo([FromQuery] string cityName)
    {
        var cityAdminStaff =
             await unitOfWork.CityAdminStaffRepo.FindAsync(C => C.City == cityName, false, ["MainInventories", "User"]);

        var cityAdminId = cityAdminStaff.UserId;

        if (cityAdminStaff is null)
            throw new NotFoundBadRequest("لا يوجد ادمن لهذه المدينه");


        var cityMainInventoryDto = cityAdminStaff.MainInventories.Adapt<List<MainInventoryDto>>();

        var cityDetails = cityAdminStaff.User.Adapt<CityRecordDetailsDto>();
        
        cityDetails.VaccineInventory = cityMainInventoryDto;

        var response = new GeneralResponse<CityRecordDetailsDto>(StatusCodes.Status200OK, cityDetails);

        return Ok(response);
    }

    [HttpGet("city-admins")]
    public IActionResult CityAdmins()
    {
        var governorateAdminId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var cityAdmins =
            unitOfWork.CityAdminStaffRepo.FindByCondition(admin => admin.GovernorateAdminId == governorateAdminId, false, "User")
            .Select(admin => new CityAdminsDto(admin.UserId, admin.User.FirstName, admin.User.LastName, admin.User.Email, admin.City))
            .ToList();
            

        if (cityAdmins is null)
            throw new NotFoundException("لا يوجد مستخدمين مراكز لهذه المحافظه يمكنك اضافه مستخدمين");

        var response = new GeneralResponse<List<CityAdminsDto>>(StatusCodes.Status200OK, cityAdmins);

        return Ok(response);
    }

    [HttpPost("city-admin-add")]
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

    [HttpPost("governorate-inventory-create")]
    public async Task<IActionResult> EstablishInventory()
    {
        var governorateAdminId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var governorateAdmin = 
            await unitOfWork.GovernorateAdminRepo.FindAsync(admin => admin.UserId == governorateAdminId, false);


        var inventoryExists = await unitOfWork.MainInventoryRepo.ExistsAsync(inv => inv.GovernorateAdminStaffId == governorateAdminId);

        if (inventoryExists)
            throw new BadRequestException("تم انشاء محزن لهذا المستخدم من قبل");

        var antigens = Utility.InventoryAntigens();

        List<MainInventory> inventoryAntigens = [];

        foreach (var antigen in antigens)
        {
            var mainInventory = new MainInventory
            {
                InventoryLocation = governorateAdmin.Governorate,
                Antigen = antigen,
                GovernorateAdminStaffId = governorateAdminId,
                Amount = 0,
            };

            inventoryAntigens.Add(mainInventory);
        } 

        await unitOfWork.MainInventoryRepo.AddRangeAsync(inventoryAntigens);
        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            throw new BadRequestException("لم يتم حفظ بيانات المخزن الرجاء المحاوله مره اخري");

        var response = new GeneralResponse<List<MainInventory>>(StatusCodes.Status200OK, inventoryAntigens);

        return Ok(response);
    }

    [HttpGet("governorate-inventory")]
    public IActionResult GovernorateInventory()
    {
        var governorateAdminId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var inventory =
            unitOfWork.MainInventoryRepo.FindByCondition(inv => inv.GovernorateAdminStaffId == governorateAdminId, false).ToList() ?? [];

        var invenrotyDto = inventory.Adapt<List<MainInventoryDto>>();

        var response = new GeneralResponse<List<MainInventoryDto>>(StatusCodes.Status200OK, invenrotyDto);

        return Ok(response);
    }

    [HttpDelete("{cityAdminId:guid}/remove-admin")]
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