using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.Shared.DTOs.SignalRDtos;
using Ebret4m4n.Shared.DTOs.ChatDtos;
using Microsoft.AspNetCore.Mvc;
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
        var parentId = User.FindFirst("id")!.Value;

        var messages = unitOfWork.ChatRepo.FindByCondition(message => message.SenderId == parentId
        && message.ReceiverId == reciverId.ToString(), false)
            .ToList();

        var messagesDto = messages.Adapt<List<ChatMessageDetailsDto>>();

        var response = GeneralResponse<List<ChatMessageDetailsDto>>.SuccessResponse(messagesDto);

        return Ok(response);
    }

    [HttpGet("parent-chat-list")]
    public IActionResult UserChatList()
    {
        var doctorId = User.FindFirst("id")!.Value;

        var userChatList = unitOfWork.ChatRepo.FindByCondition(chat => chat.ReceiverId == doctorId, false, ["Sender"])
            .Distinct()
            .Select(u => u.Adapt<ChatUsersListDto>())
            .ToList() ?? [];

        var response = GeneralResponse<List<ChatUsersListDto>>.SuccessResponse(userChatList);

        return Ok(response);
    }
}
