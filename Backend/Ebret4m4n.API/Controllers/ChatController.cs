using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.Shared.DTOs.SignalRDtos;
using Ebret4m4n.Shared.DTOs.ChatDtos;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ebret4m4n.Shared.DTOs;
using Ebret4m4n.Contracts;
using Mapster;

namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ChatController(IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet("{reciverId:guid}/user-messages")]
    public IActionResult GetChatMessages(Guid reciverId)
    {
        var parentId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var messages = unitOfWork.ChatRepo.FindByCondition(message => message.SenderId == parentId
        && message.ReceiverId == reciverId.ToString(), false)
            .ToList();

        var messagesDto = messages.Adapt<List<ChatMessageDetailsDto>>();

        var response = new GeneralResponse<List<ChatMessageDetailsDto>>(StatusCodes.Status200OK, messagesDto);

        return Ok(response);
    }

    [HttpGet("parent-chat-list")]
    public IActionResult UserChatList()
    {
        var doctorId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var userChatList = unitOfWork.ChatRepo.FindByCondition(chat => chat.ReceiverId == doctorId, false, ["Sender"])
            .Distinct()
            .Select(u => u.Adapt<ChatUsersListDto>())
            .ToList() ?? [];

        var response = new GeneralResponse<List<ChatUsersListDto>>(StatusCodes.Status200OK, userChatList);

        return Ok(response);
    }
}
