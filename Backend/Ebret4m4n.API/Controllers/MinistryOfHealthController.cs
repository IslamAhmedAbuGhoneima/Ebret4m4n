using Ebret4m4n.Shared.DTOs.AdminsDto.GovernorateAdminDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ebret4m4n.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using Ebret4m4n.Entities.Models;
using Ebret4m4n.Entities.Exceptions;
using Microsoft.AspNetCore.SignalR;
using Ebret4m4n.API.Utilites;
using Ebret4m4n.Contracts;
using Ebret4m4n.API.Hubs;
using Mapster;


namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "admin")]
[ApiController]
public class MinistryOfHealthController
    (IUnitOfWork unitOfWork,
    UserManager<ApplicationUser> userManager,
    IHubContext<NotificationHub> hubContext) : ControllerBase
{
    [HttpGet("governorates")]
    public IActionResult GetGovernorates()
    {
        var governorates = unitOfWork.GovernorateAdminRepo.FindAll(false)
            .Select(gov => gov.Governorate)
            .ToList();

        var response = new GeneralResponse<List<string>>(StatusCodes.Status200OK, governorates);

        return Ok(response);
    }

    [HttpGet("governorate-details")]
    public async Task<IActionResult> GovernorateDetails([FromQuery] string governorateName)
    {
        var governorate =
            await unitOfWork.GovernorateAdminRepo.FindAsync(gov => gov.Governorate == governorateName, false, ["User", "MainInventories", "CityAdminStaffs"]);

        var governorateDetails = governorate.Adapt<GovernorateDetailsDto>();

        var response = new GeneralResponse<GovernorateDetailsDto>(StatusCodes.Status200OK, governorateDetails);

        return Ok(response);
    }

    [HttpPost("add-governorate-admin")]
    public async Task<IActionResult> AddGovernorateAdmin([FromBody] AddGovernorateAdminDto model)
    {
        if(!ModelState.IsValid)
            return UnprocessableEntity(
                new GeneralResponse<string>(StatusCodes.Status422UnprocessableEntity,"تاكد من ان جميع البيانات المدخله صحيحه"));


        await unitOfWork.BeginTransactionAsync();

        try
        {
            var user = model.Adapt<ApplicationUser>();

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                throw new BadRequestException("لم يتم انشاء ادمن محافظه");

            var roleResult = await userManager.AddToRoleAsync(user, "governorateAdmin");

            if(!roleResult.Succeeded)
                throw new BadRequestException("لم يتم اضافه الادمن للدور");

            var governorateAdmin = new GovernorateAdminStaff() { Governorate = model.Governorate, UserId = user.Id };

            await unitOfWork.GovernorateAdminRepo.AddAsync(governorateAdmin);

            var dbResult = await unitOfWork.SaveAsync();

            if (dbResult == 0)
                throw new BadRequestException("لم يتم حفظ البيانات");

            var response = new GeneralResponse<string>(StatusCodes.Status200OK, "تم اضافه هذا الادمن بنجاح");

            await unitOfWork.CommitTransactionAsync();

            return Ok(response);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            return StatusCode(StatusCodes.Status500InternalServerError,new GeneralResponse<string>(StatusCodes.Status500InternalServerError,$"{ex.Message} :حدث خطا ما اثناء تسجيل البينات"));
        }
    }

    [HttpGet("governorate-admins")]
    public IActionResult GovernorateAdmins()
    {
        var admins = unitOfWork.GovernorateAdminRepo.FindAll(false, ["User"])
            .Select(admin => admin.Adapt<GovernorateAdminsDto>())
            .ToList() ?? [];

        var response = new GeneralResponse<List<GovernorateAdminsDto>>(StatusCodes.Status200OK, admins);

        return Ok(response);
    }

    [HttpPost("{orderId:guid}/accept-order-request")]
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
                throw new BadRequestException("لم يتم حفظ البيانات");

            await hubContext.Clients.User(order.GovernorateAdminStaffId!).SendAsync("NotificationMessage");

            await unitOfWork.CommitTransactionAsync();

            var response = new GeneralResponse<string>(StatusCodes.Status200OK, "تم قبول الطلب بنجاح");

            return Ok(response);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            return StatusCode(StatusCodes.Status500InternalServerError, new GeneralResponse<string>(StatusCodes.Status500InternalServerError, $"حدث خطا ما اثناء تسجيل البينات: {ex.Message}"));
        }
    }
}