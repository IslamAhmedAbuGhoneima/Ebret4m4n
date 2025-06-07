using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.Shared.DTOs.SignalRDtos;
using Ebret4m4n.Shared.DTOs.ChatDtos;
using Microsoft.AspNetCore.Mvc;
using Ebret4m4n.Shared.DTOs;
using Ebret4m4n.Contracts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ChatController(IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet("{reciverId:guid}/user-messages")]
    public async Task<IActionResult> GetChatMessages(Guid reciverId)
    {
        var parentId = User.FindFirst("id")!.Value;

        // Then get all messages
        var messages = await unitOfWork.ChatRepo.FindByCondition(message =>
            (message.SenderId == parentId && message.ReceiverId == reciverId.ToString()) ||
            (message.SenderId == reciverId.ToString() && message.ReceiverId == parentId), false)
            .OrderBy(message => message.SentAt)
            .ToListAsync();

        var messagesDto = messages.Adapt<List<ChatMessageDetailsDto>>();

        var response = GeneralResponse<List<ChatMessageDetailsDto>>.SuccessResponse(messagesDto);

        return Ok(response);
    }

    [HttpGet("user-chat-list")]
    public IActionResult UserChatList()
    {
        var userId = User.FindFirst("id")!.Value;

        var chats = unitOfWork.ChatRepo.FindByCondition(chat => chat.ReceiverId == userId, false, ["Sender"])
            .ToList();

        var userChatList = chats
            .GroupBy(chat => chat.SenderId)
            .Select(group => {
                var lastMessage = group.OrderByDescending(c => c.SentAt).First();
                var unreadCount = group.Count(c => !c.IsRead);
                var dto = lastMessage.Adapt<ChatUsersListDto>();
                return dto with { UnreadCount = unreadCount };
            })
            .ToList();

        var response = GeneralResponse<List<ChatUsersListDto>>.SuccessResponse(userChatList);

        return Ok(response);
    }
}
