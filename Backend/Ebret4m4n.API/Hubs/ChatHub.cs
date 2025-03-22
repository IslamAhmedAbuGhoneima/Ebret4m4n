using Ebret4m4n.Entities.Models;
using Ebret4m4n.Shared.DTOs.SignalRDtos;
using Microsoft.AspNetCore.SignalR;
using Ebret4m4n.Contracts;
using Mapster;

namespace Ebret4m4n.API.Hubs;

public class ChatHub
    (IUnitOfWork unitOfWork) : Hub
{
    public async Task SendMessage(ChatMessageDto message)
    {
        var chat = message.Adapt<Chat>();

        await unitOfWork.ChatRepo.AddAsync(chat);

        await unitOfWork.SaveAsync();

        var chatDto = chat.Adapt<ChatMessageDto>();

        await Clients.Users(chat.SenderId, chat.ReceiverId).SendAsync("ReceiveMessage", chatDto);
    }
}
