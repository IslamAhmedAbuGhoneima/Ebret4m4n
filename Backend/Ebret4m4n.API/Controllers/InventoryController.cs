﻿using Ebret4m4n.Shared.DTOs.InventoriesDtos;
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
[ApiController]
public class InventoryController
    (IUnitOfWork unitOfWork): ControllerBase
{
    [HttpPost("admins-create-inventory")]
    [Authorize(Roles = "governorateAdmin,cityAdmin")]
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
    [Authorize(Roles = "governorateAdmin,cityAdmin")]
    public IActionResult GetAdminsInventory()
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

    [HttpPost("organizer-create-inventory")]
    [Authorize(Roles = "organizer")]
    public async Task<IActionResult> EstablishOrgnizerInventory()
    {
        var organizerHCId = User.FindFirst("healthCareId")!.Value;

        var antigens = Utility.InventoryAntigens();

        List<Inventory> inventoryAntigens = [];

        foreach(var antigen in antigens)
        {
            var inventory = new Inventory
            {
                Antigen = antigen,
                Amount = 0,
                HealthCareCenterId = Guid.Parse(organizerHCId)
            };
            inventoryAntigens.Add(inventory);
        }

        await unitOfWork.InventoryRepo.AddRangeAsync(inventoryAntigens);

        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            throw new BadRequestException("لم يتم حفظ بيانات المخزن الرجاء المحاوله مره اخري");

        var response = new GeneralResponse<string>(StatusCodes.Status200OK, "تم انشاء المخزن بنجاح");

        return Ok(response);
    }

    [HttpGet("get_orgnizer_inventory")]
    [Authorize(Roles = "organizer")]
    public IActionResult GetOrganizerInventory()
    {
        var organizerHCId = User.FindFirst("healthCareId")!.Value;

        var inventory =
             unitOfWork.InventoryRepo.FindByCondition(inv => inv.HealthCareCenterId.ToString() == organizerHCId, false)
             .ToList() ?? [];

        if (inventory == null)
            throw new NotFoundBadRequest("لم يتم انشاء مخزن ل هذه الوحدة");

        var inventoryDto = inventory.Adapt<List<InventoryDto>>();

        var response = new GeneralResponse<List<InventoryDto>>(StatusCodes.Status200OK, inventoryDto);

        return Ok(response);
    }
}
