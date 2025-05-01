using Ebret4m4n.Shared.DTOs.NotificationDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ebret4m4n.Shared.DTOs;
using Ebret4m4n.Contracts;
using Mapster;

namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class NotificationController
    (IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet("notifications")]
    public IActionResult Notifications()
    {
        var userId = User.FindFirst("id")!.Value;

        var notification =
            unitOfWork.NotificationRepo.FindByCondition(not => not.UserId == userId && not.IsRead == false, false)
            .ToList() ?? [];

        var notificationDto = notification.Adapt<List<NotificatioDto>>();

        var response = GeneralResponse<List<NotificatioDto>>.SuccessResponse(notificationDto);

        return Ok(response);
    }
}
