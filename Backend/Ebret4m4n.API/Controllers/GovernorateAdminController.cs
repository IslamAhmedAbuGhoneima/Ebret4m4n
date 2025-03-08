using Ebret4m4n.Shared.DTOs.AdminsDto.CityAdminDots;
using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.Entities.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ebret4m4n.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Ebret4m4n.Contracts;
using Ebret4m4n.Shared.DTOs;
using Mapster;
using System.Security.Claims;
using Ebret4m4n.Shared.DTOs.OrderDtos;
using System.Text.Json;

namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "governorateAdmin")]
[ApiController]
public class GovernorateAdminController
    (IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : ControllerBase
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

    // Review
    [HttpGet("city-details")]
    public async Task<IActionResult> CityInfo([FromQuery] string cityName)
    {
        var cityAdminId =
             (await unitOfWork.CityAdminStaffRepo.FindAsync(C => C.City == cityName, false, "MainInventories")).UserId;

        if (cityAdminId is null)
            throw new NotFoundBadRequest("لا يوجد ادمن لهذه المدينه");

        var cityAdmin = await userManager.FindByIdAsync(cityAdminId);

        if (cityAdmin is null)
            throw new BadRequestException("لا يوجد مدير لهذه المدينه الرجاء اضافه مدير");

        var cityDetails = cityAdmin.Adapt<CityRecordDetailsDto>();

        var response = new GeneralResponse<CityRecordDetailsDto>(StatusCodes.Status200OK, cityDetails);

        return Ok(response);
    }

    [HttpGet("city-admins")]
    public IActionResult CityAdmins()
    {
        var governorate = User.FindFirst("governorate")!.Value;

        var cityAdmins =
            unitOfWork.CityAdminStaffRepo.FindByCondition(admin => admin.Governorate == governorate, false, "User")
            .Select(admin => admin.User.UserName)
            .ToList();

        if (cityAdmins is null)
            throw new NotFoundException("لا يوجد مستخدمين مراكز لهذه المحافظه يمكنك اضافه مستخدمين");

        var response = new GeneralResponse<List<string>>(StatusCodes.Status200OK, cityAdmins);

        return Ok(response);
    }

    [HttpPost("city-admin-add")]
    public async Task<IActionResult> AddCityAdmin(AddCityAdminDto model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);


        var user = model.Adapt<ApplicationUser>();

        var result = await userManager.CreateAsync(user, model.Password);

        if(!result.Succeeded)
            throw new BadRequestException("لم يتم انشاء هذا المستخدم");

        await userManager.AddToRoleAsync(user, model.Role);
        
        var cityAdmin = model.Adapt<CityAdminStaff>();
        cityAdmin.UserId = user.Id;


        await unitOfWork.CityAdminStaffRepo.AddAsync(cityAdmin);
        var dbResult = await unitOfWork.SaveAsync();

        if (dbResult == 0)
            throw new BadRequestException("لم يتم حفظ البيانات الخاصه بهذا المستخدم");

        var response = new GeneralResponse<string>(StatusCodes.Status200OK, "تم اضافه ادمن مدينه بنجاح");

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

        unitOfWork.CityAdminStaffRepo.Remove(cityAdmin);
        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            throw new BadRequestException("لم يتم حذف هذا المستخدم حاول مره اخري");

        var response = new GeneralResponse<string>(StatusCodes.Status204NoContent, "تم حذف هذا المستخدم");

        return Ok(response);
    }

    [HttpPost("governorate-inventory-create")]
    public async Task<IActionResult> EstablishInventory()
    {
        var governorateAdminId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var governorateAdmin = 
            await unitOfWork.GovernorateAdminRepo.FindAsync(admin => admin.UserId == governorateAdminId, false);

        var antigenFilePath = Path.Combine(Directory.GetCurrentDirectory(), "ChildBaseVaccines", "Antigens.json");

        if (!Path.Exists(antigenFilePath))
            throw new NotFoundException("لم يتم العثور علي السمتضاتات الخاصه بالقاحات");

        using var stream = new FileStream(antigenFilePath, FileMode.Open);

        var antigens = JsonSerializer.Deserialize<List<string>>(stream);

        if (antigens == null)
            throw new BadRequestException("لم ايجاد اي مستضادات لقاح");

        List<MainInventory> inventoryAntigens = [];

        foreach(var antigen in antigens)
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

    [HttpPost("request-order")]
    public async Task<IActionResult> RequestOrder([FromQuery] string antigenName, [FromQuery] uint amount)
    {
        var governorateAdminId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var antigen =
            await unitOfWork.MainInventoryRepo.FindAsync(inventory => inventory.GovernorateAdminStaffId == governorateAdminId &&
            inventory.Antigen == antigenName, false);

        if (antigen == null)
            throw new BadRequestException("لا يوجد مستضادات بهذا الاسم");

        var order = 
            new Order() { Antigen = antigenName, Amount = amount,Status = OrderStatus.Pending, GovernorateAdminStaffId = governorateAdminId };

        await unitOfWork.OrderRepo.AddAsync(order);
        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            throw new BadRequestException("لم يتم حفظ الطلب الرجاء المحاوله مره اخري");

        var response = new GeneralResponse<string>(StatusCodes.Status200OK, "تم اضافه الطلب بنجاح");

        return Ok(response);
    }

    [HttpGet("governorate-antigens-order-pending")]
    public IActionResult PendingOrders()
    {
        var pendingOrders = Orders(OrderStatus.Pending);
        var response = new GeneralResponse<List<OrderDto>>(StatusCodes.Status200OK, pendingOrders);
        return Ok(response);
    }

    [HttpGet("governorate-antigens-order-accepted")]
    public IActionResult AcceptedOrders()
    {
        var acceptedOrders = Orders(OrderStatus.Accepted);
        var response = new GeneralResponse<List<OrderDto>>(StatusCodes.Status200OK, acceptedOrders);
        return Ok(response);
    }

    [HttpGet("governorate-antigens-order-recived")]
    public IActionResult RecivedOrders()
    {
        var recivedOrders = Orders(OrderStatus.Recived);
        var response = new GeneralResponse<List<OrderDto>>(StatusCodes.Status200OK, recivedOrders);
        return Ok(response);
    }


    private List<OrderDto> Orders(OrderStatus status)
    {
        var goveronrateAdminId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var pendingOrders =
            unitOfWork.OrderRepo.FindByCondition(order => order.GovernorateAdminStaffId == goveronrateAdminId &&
            order.Status == status, false);

        var ordersDto = pendingOrders.Adapt<List<OrderDto>>();

        return ordersDto;
    }
}
