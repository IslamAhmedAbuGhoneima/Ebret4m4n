using Ebret4m4n.Shared.DTOs.InventoriesDtos;
using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.Entities.Exceptions;
using Ebret4m4n.API.Utilites;
using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;
using Ebret4m4n.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Mapster;

namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "governorateAdmin,cityAdmin")]
[ApiController]
public class InventoryController
    (IUnitOfWork unitOfWork): ControllerBase
{
    [HttpPost("inventory-create")]
    public async Task<IActionResult> EstablishInventory()
    {
        var adminId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var adminRole = User.FindFirst(ClaimTypes.Role)!.Value;
        CityAdminStaff? cityAdmin = null;
        GovernorateAdminStaff? governorateAdmin = null;

        var inventoryExists = await unitOfWork.MainInventoryRepo.ExistsAsync(inv => inv.GovernorateAdminStaffId == adminId || inv.CityAdminStaffId == adminId);

        if (inventoryExists)
            throw new BadRequestException("تم انشاء محزن لهذا المستخدم من قبل");


        if (adminRole == "cityAdmin")
            cityAdmin = await unitOfWork.CityAdminStaffRepo.FindAsync(c => c.UserId == adminId, false);
        else
            governorateAdmin = await unitOfWork.GovernorateAdminRepo.FindAsync(g => g.UserId == adminId, false);

        var antigens = Utility.InventoryAntigens();

        List<MainInventory> inventoryAntigens = [];

        foreach (var antigen in antigens)
        {
            var mainInventory = new MainInventory
            {
                Antigen = antigen,
                Amount = 0,
            };

            if (adminRole == "cityAdmin")
            {
                mainInventory.CityAdminStaffId = adminId;
                mainInventory.InventoryLocation = $"مدينه: {cityAdmin!.City}";
            }
            else
            {
                mainInventory.GovernorateAdminStaffId = adminId;
                mainInventory.InventoryLocation = $"محافظه: {governorateAdmin!.Governorate}";
            }
            inventoryAntigens.Add(mainInventory);
        }

        await unitOfWork.MainInventoryRepo.AddRangeAsync(inventoryAntigens);
        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            throw new BadRequestException("لم يتم حفظ بيانات المخزن الرجاء المحاوله مره اخري");


        var inventoryDto = inventoryAntigens.Adapt<List<InventoryDto>>();

        var response = new GeneralResponse<List<InventoryDto>>(StatusCodes.Status200OK, inventoryDto);

        return Ok(response);
    }

    [HttpGet("inventory")]
    public IActionResult Inventory()
    {
        var adminId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var adminRole = User.FindFirst(ClaimTypes.Role)!.Value;


        var inventory = adminRole == "cityAdmin" ?
            unitOfWork.MainInventoryRepo.FindByCondition(inv => inv.CityAdminStaffId == adminId, false).ToList() ?? [] :
            unitOfWork.MainInventoryRepo.FindByCondition(inv => inv.GovernorateAdminStaffId == adminId, false).ToList() ?? [];

        if (inventory is null) 
            throw new BadRequestException("لم يتم العثور علي بيانات المخزن او لم يتم انشائه");

        var invenrotyDto = inventory.Adapt<List<InventoryDto>>();

        var response = new GeneralResponse<List<InventoryDto>>(StatusCodes.Status200OK, invenrotyDto);

        return Ok(response);
    }
}
