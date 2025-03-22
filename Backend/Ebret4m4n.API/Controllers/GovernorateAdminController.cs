using Ebret4m4n.Shared.DTOs.AdminsDto.CityAdminDots;
using Microsoft.AspNetCore.Authorization;           
using Ebret4m4n.Shared.DTOs.OrderDtos;              
using Ebret4m4n.Entities.Exceptions;                
using Microsoft.AspNetCore.Identity;                
using Microsoft.EntityFrameworkCore;                
using Ebret4m4n.Entities.Models;                    
using Microsoft.AspNetCore.Mvc;                     
using System.Security.Claims;                       
using Ebret4m4n.API.Utilites;                       
using Ebret4m4n.Shared.DTOs;                        
using Ebret4m4n.Contracts;                          
using Mapster;                                      
                                                        

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

    [HttpGet("city-details")]
    public async Task<IActionResult> CityInfo([FromQuery] string cityName)
    {
        var cityAdminStaff =
             await unitOfWork.CityAdminStaffRepo.FindAsync(C => C.City == cityName, false, ["MainInventories"]);

        var cityAdminId = cityAdminStaff.UserId;

        if (cityAdminStaff is null)
            throw new NotFoundBadRequest("لا يوجد ادمن لهذه المدينه");

        var cityAdmin = await userManager.FindByIdAsync(cityAdminId);

        if (cityAdmin is null)
            throw new BadRequestException("لا يوجد مدير لهذه المدينه الرجاء اضافه مدير");

        var cityMainInventoryDto = cityAdminStaff.MainInventories.Adapt<List<MainInventoryDto>>();

        var cityDetails = cityAdmin.Adapt<CityRecordDetailsDto>();
        
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
            .Select(admin => new CityAdminsDto(admin.UserId, string.Join(' ', admin.User.FirstName, admin.User.LastName)))
            .ToList();
            

        if (cityAdmins is null)
            throw new NotFoundException("لا يوجد مستخدمين مراكز لهذه المحافظه يمكنك اضافه مستخدمين");

        var response = new GeneralResponse<List<CityAdminsDto>>(StatusCodes.Status200OK, cityAdmins);

        return Ok(response);
    }

    [HttpGet("requested-city-orders")]
    public IActionResult RequestedCityOrders()
    {
        var governorateAdminId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var cityAdminsOrders =
            unitOfWork.OrderRepo.FindByCondition(order => order.CityAdminStaff.GovernorateAdminId == governorateAdminId, false, ["GovernorateAdminStaff"])
            .ToList();

        var cityAdminsOrdersDto = cityAdminsOrders.Adapt<List<OrderDto>>();

        var response = new GeneralResponse<List<OrderDto>>(StatusCodes.Status200OK, cityAdminsOrdersDto);

        return Ok(response);
    }

    [HttpGet("governorate-orders")]
    public IActionResult Orders()
    {
        var goveronrateAdminId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var orders =
            unitOfWork.OrderRepo.FindByCondition(order => order.GovernorateAdminStaffId == goveronrateAdminId, false);

        var ordersDto = orders.Adapt<List<OrderDto>>();

        var response = new GeneralResponse<List<OrderDto>>(StatusCodes.Status200OK, ordersDto);

        return Ok(response);
    }

    [HttpPut("{orderId:guid}/marke-received-order")]
    public async Task<IActionResult> MarkeReceivedOrder(Guid orderId)
    {
        var governorateAdminId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var order =
            await unitOfWork.OrderRepo.FindAsync(order => order.Id == orderId, true);

        if (order is null)
            throw new NotFoundBadRequest($"لم نتمكن من ايجاد هذ الطلب");

        var governorateInventory = await unitOfWork.MainInventoryRepo.FindAsync(inv => inv.GovernorateAdminStaffId == governorateAdminId
        && inv.Antigen == order.Antigen, true);

        governorateInventory.Amount += order.Amount;
        order.Status = OrderStatus.Recived;

        unitOfWork.MainInventoryRepo.Update(governorateInventory);
        unitOfWork.OrderRepo.Update(order);

        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            throw new BadRequestException("لم يتم حفظ البيانات حاول مره اخري");

        var response = new GeneralResponse<string>(StatusCodes.Status204NoContent, "تم استلام اللقاح بنجاح :)");

        return Ok(response);
    }

    [HttpPost("city-admin-add")]
    public async Task<IActionResult> AddCityAdmin(AddCityAdminDto model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

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
            new Order() { Antigen = antigenName, Amount = amount, Status = OrderStatus.Pending, GovernorateAdminStaffId = governorateAdminId };

        await unitOfWork.OrderRepo.AddAsync(order);
        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            throw new BadRequestException("لم يتم حفظ الطلب الرجاء المحاوله مره اخري");

        var response = new GeneralResponse<string>(StatusCodes.Status200OK, "تم اضافه الطلب بنجاح");

        return Ok(response);
    }

    [HttpPost("{orderId:guid}/send-vaccine-order")]
    public async Task<IActionResult> SendVaccineOrder(Guid orderId)
    {
        var governorateAdminId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var order =
            await unitOfWork.OrderRepo.FindAsync(order => order.Id == orderId, true);

        if (order is null)
            throw new NotFoundBadRequest("لم يتم ايجاد هذا الطلب");

        var governorateInventory = await unitOfWork.MainInventoryRepo.FindAsync(inv => inv.GovernorateAdminStaffId == governorateAdminId &&
        inv.Antigen == order.Antigen, true);


        if (governorateInventory.Amount < order.Amount)
            throw new BadRequestException("الكميه الموجوده لهذا اللقاح غير كافيه لارسال هذا الطلب");

        governorateInventory.Amount -= order.Amount;
        order.Status = OrderStatus.Processing;

        unitOfWork.OrderRepo.Update(order);
        unitOfWork.MainInventoryRepo.Update(governorateInventory);

        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            throw new BadRequestException("لم يتم حفظ البيانات");

        // send notification to city admin that his order is proccessing

        var response = new GeneralResponse<string>(StatusCodes.Status200OK, "تم قبول الطلب بنجاح جاري الارسال");

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
}
