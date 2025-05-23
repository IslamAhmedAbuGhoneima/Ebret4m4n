﻿using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.Shared.DTOs.SignalRDtos;
using Ebret4m4n.Shared.DTOs.OrderDtos;
using Ebret4m4n.Entities.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Ebret4m4n.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Ebret4m4n.API.Utilites;
using System.Security.Claims;
using Ebret4m4n.Shared.DTOs;
using Ebret4m4n.Contracts;
using Ebret4m4n.API.Hubs;
using Mapster;

namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController
    (IUnitOfWork unitOfWork,
    UserManager<ApplicationUser> userManager,
    IHubContext<NotificationHub> hubContext): ControllerBase
{
    [HttpGet("orders")]
    [Authorize(Roles = "governorateAdmin,cityAdmin,organizer")]
    public IActionResult Orders()
    {
        var adminId = User.FindFirst("id")!.Value;
        var adminRole = User.FindFirst(ClaimTypes.Role)!.Value;

        var orders =
            adminRole == "governorateAdmin" ?
                unitOfWork.OrderRepo.FindByCondition(order => order.GovernorateAdminStaffId == adminId, false) :
            adminRole == "cityAdmin" ?
                unitOfWork.OrderRepo.FindByCondition(order => order.CityAdminStaffId == adminId, false) :
                unitOfWork.OrderRepo.FindByCondition(order => order.MedicalStaffId == adminId, false);

        var ordersDto = orders.Adapt<List<MyOrderDetailsDto>>();

        var response = GeneralResponse<List<MyOrderDetailsDto>>.SuccessResponse(ordersDto);

        return Ok(response);
    }

    [HttpGet("{orderId:guid}/order-details")]
    [Authorize(Roles = "admin,governorateAdmin,cityAdmin,organizer")]
    public IActionResult OrderDetails(Guid orderId)
    {
        var orderItems =
            unitOfWork.OrderItemRepo.FindByCondition(item => item.OrderId == orderId, false)
            .ToList();

        if (orderItems is null)
            return BadRequest(GeneralResponse<string>.FailureResponse("لايوجد طلبات بهذا الرقم"));

        var orderItemsDto = orderItems.Adapt<List<OrderItemsDto>>();

        var response = GeneralResponse<List<OrderItemsDto>>.SuccessResponse(orderItemsDto);

        return Ok(response);
    }

    [HttpPost("request-order")]
    [Authorize(Roles = "governorateAdmin,cityAdmin,organizer")]
    public async Task<IActionResult> RequestOrder(List<OrderItemsDto> model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(GeneralResponse<string>.FailureResponse("الرجاء التاكد من المدخلات"));

        var adminId = User.FindFirst("id")!.Value;
        var adminRole = User.FindFirst(ClaimTypes.Role)!.Value;

        await unitOfWork.BeginTransactionAsync();

        try
        {
            var order = new Order();

            _ = adminRole == "governorateAdmin" ?
                    order.GovernorateAdminStaffId = adminId :
                adminRole == "cityAdmin" ?
                    order.CityAdminStaffId = adminId :
                    order.MedicalStaffId = adminId;

            await unitOfWork.OrderRepo.AddAsync(order);

            var orderSaveResult = await unitOfWork.SaveAsync();
            if (orderSaveResult == 0)
                return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم حفظ بيانات الطلب"));

            var saveItemsResult = await SaveOrderItems(model, order.Id);

            if (!saveItemsResult)
                return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم حفظ عناصر الطلب"));

            await unitOfWork.CommitTransactionAsync();

            var response = GeneralResponse<string>.SuccessResponse("تم حفظ الطلب بنجاح");

            return Ok(response);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            var response =  GeneralResponse<string>.FailureResponse($"{ex.Message}:حدث خطأ أثناء حفظ الطلب");
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    [HttpPut("{orderId:guid}/marke-received-order")]
    [Authorize(Roles = "governorateAdmin,cityAdmin")]
    public async Task<IActionResult> MarkeReceivedOrder(Guid orderId)
    {
        var adminId = User.FindFirst("id")!.Value;
        var adminRole = User.FindFirst(ClaimTypes.Role)!.Value;

        await unitOfWork.BeginTransactionAsync();

        try
        {
            var order =
            await unitOfWork.OrderRepo
            .FindAsync(order => order.Id == orderId, true, ["OrderItems", "CityAdminStaff", "GovernorateAdminStaff", "MedicalStaff"]);

            if (order is null)
                return NotFound(GeneralResponse<string>.FailureResponse("لم نتمكن من ايجاد هذ الطلب"));

            if (order.Status == OrderStatus.Recived)
                return BadRequest(GeneralResponse<string>.FailureResponse("تم استلام هذا الطلب من قبل"));

            var inventory = GetInventory(adminRole, adminId);

            var orderItems = order.OrderItems.ToList();

            var updateResult = UpdateInventory(orderItems, inventory, '+');

            if (!updateResult)
                return BadRequest(GeneralResponse<string>.FailureResponse("هناك خطأ ما في هذا الطلب، راجع بيانات الطلب والمخزن"));

            order.Status = OrderStatus.Recived;
            order.DateApproved = DateTime.UtcNow;
            unitOfWork.OrderRepo.Update(order);

            var notification = await Notification(adminRole, order);

            await unitOfWork.NotificationRepo.AddAsync(notification);

            int result = await unitOfWork.SaveAsync();

            if (result == 0)
                return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم تغير حاله الطلب حوال مره اخري"));

            await unitOfWork.CommitTransactionAsync();

            var notificationDto = notification.Adapt<NotificationDto>();

            await SendNotification(notification.UserId, notificationDto);

            return NoContent();
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            return StatusCode(StatusCodes.Status500InternalServerError,
             GeneralResponse<string>.FailureResponse($"{ex.Message} :حدث خطأ أثناء معالجة الطلب"));
        }

    }

    [HttpPost("{orderId:guid}/accept-vaccine-order")]
    [Authorize(Roles = "governorateAdmin,cityAdmin")]
    public async Task<IActionResult> AcceptVaccineOrder(Guid orderId)
    {
        var adminId = User.FindFirst("id")!.Value;
        var adminRole = User.FindFirst(ClaimTypes.Role)!.Value;

        await unitOfWork.BeginTransactionAsync();

        try
        {
            var order =
            await unitOfWork.OrderRepo.FindAsync(order => order.Id == orderId, true, ["OrderItems"]);

            if (order is null)
                return NotFound(GeneralResponse<string>.FailureResponse("لم يتم ايجاد هذا الطلب"));

            var inventory = GetInventory(adminRole, adminId);

            var orderItems = order.OrderItems.ToList();

            var updateInventory = UpdateInventory(orderItems, inventory, '-');

            if (!updateInventory)
                return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم تحديث بيانات المخزن"));

            order.Status = OrderStatus.Processing;
            unitOfWork.OrderRepo.Update(order);


            var notification =
                 Utility.CreateNotification("اللقاحات", "تم قبول طلب القاحات الخاص بك", adminRole == "governorateAdmin" ? order.CityAdminStaffId! : order.MedicalStaffId!);

            await unitOfWork.NotificationRepo.AddAsync(notification);

            var result = await unitOfWork.SaveAsync();

            if (result == 0)
                return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم حفظ البيانات"));

            var notificationDto = notification.Adapt<NotificationDto>();

            await hubContext.Clients.User(notification.UserId).SendAsync("NotificationMessage", notificationDto);

            await unitOfWork.CommitTransactionAsync();

            var response = GeneralResponse<string>.SuccessResponse("تم قبول الطلب بنجاح جاري الارسال");

            return Ok(response);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            return StatusCode(StatusCodes.Status500InternalServerError,
                 GeneralResponse<string>.FailureResponse($"{ex.Message}:حدث خطأ أثناء ارسال الطلب"));
        }
    }

    [HttpGet("requested-healthcare-orders")]
    [Authorize(Roles = "cityAdmin")]
    public IActionResult RequestedHealthCareOrders()
    {
        var adminId = User.FindFirst("id")!.Value;

        var orders =
            unitOfWork.OrderRepo
            .FindByCondition(order => order.MedicalStaff.CityAdminStaffId == adminId, false, ["MedicalStaff"])
            .Select(order => order.Adapt<HealthCareOrdersDto>())
            .ToList() ?? [];

        return Ok(orders);
    }

    [HttpGet("requested-city-orders")]
    [Authorize(Roles = "governorateAdmin")]
    public IActionResult RequestedCityOrders()
    {
        var governorateAdminId = User.FindFirst("id")!.Value;

        var cityOrders =
            unitOfWork.OrderRepo.FindByCondition(order => order.CityAdminStaff.GovernorateAdminId == governorateAdminId, false, ["CityAdminStaff"])
            .Select(order => order.Adapt<CityOrderDetails>())
            .ToList() ?? [];

        var response = GeneralResponse<List<CityOrderDetails>>.SuccessResponse(cityOrders);

        return Ok(response);
    }

    [HttpGet("requested-governorate-orders")]
    [Authorize(Roles = "admin")]
    public IActionResult GovernorateOrders()
    {
        var governorateOrders =
            unitOfWork.OrderRepo.FindByCondition(order => order.GovernorateAdminStaffId != null, false, ["GovernorateAdminStaff"])
            .ToList() ?? [];

        var governorateOrdersDto = governorateOrders.Adapt<List<GovernorateOrderDto>>();

        var response = GeneralResponse<List<GovernorateOrderDto>>.SuccessResponse(governorateOrdersDto);

        return Ok(response);
    }

    [HttpPost("{orderId:guid}/accept-governorate-order-request")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> AcceptOrderRequest(Guid orderId)
    {
        var order = await unitOfWork.OrderRepo.FindAsync(order => order.Id == orderId, false);

        await unitOfWork.BeginTransactionAsync();

        try
        {
            order.Status = OrderStatus.Processing;

            var notification = Utility.CreateNotification("طلب جديد", "تم قبول طلب القاحات الخاص بك", order.GovernorateAdminStaffId!);

            await unitOfWork.NotificationRepo.AddAsync(notification);
            unitOfWork.OrderRepo.Update(order);


            var result = await unitOfWork.SaveAsync();

            if (result == 0)
                return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم حفظ البيانات"));

            await hubContext.Clients.User(order.GovernorateAdminStaffId!).SendAsync("NotificationMessage");

            await unitOfWork.CommitTransactionAsync();

            var response = GeneralResponse<string>.SuccessResponse("تم قبول الطلب بنجاح");

            return Ok(response);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            return StatusCode(StatusCodes.Status500InternalServerError,  GeneralResponse<string>.FailureResponse($"حدث خطا ما اثناء تسجيل البينات: {ex.Message}"));
        }
    }

    [HttpPut("{orderId:guid}/orgnizer-recived-order")]
    [Authorize(Roles = "organizer")]
    public async Task<IActionResult> OrgnizerRecivedOrder(Guid orderId)
    {
        var orgnizerHcId = User.FindFirst("healthCareId")!.Value;

        await unitOfWork.BeginTransactionAsync();
        try
        {
            var order = await unitOfWork.OrderRepo.FindAsync(order => order.Id == orderId, true, ["OrderItems"]);

            if (order is null)
                return NotFound(GeneralResponse<string>.FailureResponse("لم يتم العثور على الطلب"));

            var inventory = unitOfWork.InventoryRepo.FindByCondition(inv => inv.HealthCareCenterId.ToString() == orgnizerHcId, true)
                .ToList();

            var orderItems = order.OrderItems.ToList();

            foreach (var antigine in orderItems)
            {
                var inventoryItem = inventory.FirstOrDefault(inv => inv.Antigen == antigine.Antigen);

                if (inventoryItem is null)
                    return BadRequest($"لم يتم العثور علي العنصر : {antigine.Antigen} في المخزن");

                inventoryItem.Amount += antigine.Amount;

                unitOfWork.InventoryRepo.Update(inventoryItem);
            }

            order.Status = OrderStatus.Recived;

            unitOfWork.OrderRepo.Update(order);

            var result = await unitOfWork.SaveAsync();

            if (result == 0)
                return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم حفظ البيانات"));

            await unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();

            return StatusCode(StatusCodes.Status500InternalServerError,
                GeneralResponse<string>.FailureResponse("حدث خطا ما اثناء تسجيل البينات"));
        }

        return NoContent();
    }


    private async Task<Notification> Notification(string adminRole, Order order)
    {
        string title = "استلام الطلبات";
        string message;
        string userId;

        var notification = new Notification() { Title = title };

        if (adminRole == "cityAdmin")
        {
            message = $"بنجاح {order.CityAdminStaff.City} تم استلام الطلب الخاص بمدينه";
            userId = order.CityAdminStaff.GovernorateAdminId;
        }   
        else if (adminRole == "governorateAdmin")
        {
            var ministryOfHealhAdminId = (await userManager.GetUsersInRoleAsync("admin")).FirstOrDefault()!.Id;
            message = $"بنجاح {order.GovernorateAdminStaff.Governorate} تم استلام الطلب الخاص بمحافظه";
            userId = ministryOfHealhAdminId;
        }
        else
        {
            message = $"بنجاح {order.MedicalStaff.HealthCareCenterName} تم استلام الطلب الخاص بالوحد الصحيه";
            userId = order.MedicalStaff.CityAdminStaffId;
        }

        notification.Message = message;

        return Utility.CreateNotification(title, message, userId);
    }

    private async Task SendNotification(string userId, NotificationDto notification)
    => await hubContext.Clients.User(userId).SendAsync("NotificationMessage", notification);

    private Dictionary<string, MainInventory> GetInventory(string adminRole, string adminId)
    {
        return adminRole == "governorateAdmin"
            ? unitOfWork.MainInventoryRepo.FindByCondition(inv => inv.GovernorateAdminStaffId == adminId, true)
        .ToDictionary(inv => inv.Antigen)
            : unitOfWork.MainInventoryRepo.FindByCondition(inv => inv.CityAdminStaffId == adminId, true)
              .ToDictionary(inv => inv.Antigen);
    }

    private async Task<bool> SaveOrderItems(List<OrderItemsDto> orderItems, Guid orderId)
    {
        var items = orderItems.Adapt<List<OrderItem>>();

        items.ForEach(item => item.OrderId = orderId);

        await unitOfWork.OrderItemRepo.AddRangeAsync(items);

        return await unitOfWork.SaveAsync() > 0;
    }

    private bool UpdateInventory(List<OrderItem> items, Dictionary<string, MainInventory> inventory, char operation)
    {
        try
        {
            foreach (var item in items)
            {
                if (!inventory.TryGetValue(item.Antigen, out var inventoryItem))
                    return false;

                if (operation != '+' && operation != '-')
                    throw new BadRequestException("العملية غير مدعومة، استخدم '+' أو '-' فقط");

                inventoryItem.Amount = operation switch
                {
                    '+' => inventoryItem.Amount + item.Amount,
                    '-' when inventoryItem.Amount >= item.Amount => inventoryItem.Amount - item.Amount,
                    _ => throw new BadRequestException("لا يمكن خصم كمية أكبر من المخزون المتاح")
                };
                unitOfWork.MainInventoryRepo.Update(inventoryItem);
            }
            return true;
        }
        catch (Exception ex)
        {
            throw new BadRequestException($"خطأ أثناء تحديث المخزون: {ex.Message}");
        }
    }
}
