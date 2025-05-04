using Ebret4m4n.Shared.DTOs.NotificationDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ebret4m4n.Shared.DTOs;
using Ebret4m4n.Contracts;
using Mapster;
using System.Threading.Tasks;

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

    [HttpPost("{id:guid}/mark-as-read")]
    public async Task<IActionResult> MarckNotificationAsRead(Guid id)
    {
        var notification = await unitOfWork.NotificationRepo.FindAsync(not => not.Id == id, true);

        if (notification == null)
            return NotFound(GeneralResponse<string>.FailureResponse("لايوجد تنبيه بهذا الرقم او تنبيه تم حذفه"));

        notification.IsRead = true;

        unitOfWork.NotificationRepo.Update(notification);

        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            return BadRequest(GeneralResponse<string>.FailureResponse("حدث خطأ أثناء تحديث التنبيه"));

        var response = GeneralResponse<string>.SuccessResponse("تم تحديث التنبيه بنجاح");

        return Ok(response);
    }
}
