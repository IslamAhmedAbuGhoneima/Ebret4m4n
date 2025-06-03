using Ebret4m4n.Shared.DTOs.SignalRDtos;
using Microsoft.AspNetCore.SignalR;
using Ebret4m4n.Entities.Models;
using Ebret4m4n.Contracts;
using Microsoft.AspNetCore.Mvc;
using Ebret4m4n.API.Utilites;
using Mapster;

namespace Ebret4m4n.API.Hubs;

public class ChatHub
    (IUnitOfWork unitOfWork) : Hub
{
    public async Task SendMessage([FromForm] ChatMessageDto message)
    {
        var chat = message.Adapt<Chat>();
        chat.SentAt = DateTime.UtcNow;

        if (message.File is not null) 
            chat.File = Utility.UploadChatFile(message.File);

        await unitOfWork.ChatRepo.AddAsync(chat);

        await unitOfWork.SaveAsync();

        var chatDto = chat.Adapt<ChatMessageDetailsDto>();

        await Clients.Users([chat.SenderId!, chat.ReceiverId!]).SendAsync("ReceiveMessage", chatDto);
    }

    public async Task DeleteMessage(Guid messageId)
    {
        var chatMessage = 
            await unitOfWork.ChatRepo.FindAsync(message => message.Id == messageId, true);

        if (chatMessage is null)
        {
            await Clients.Caller.SendAsync("DeleteMessageError", "هذه الرساله غير موجوده");
            return;
        }

        if(chatMessage.SenderId != Context.UserIdentifier)
        {
            await Clients.Caller.SendAsync("DeleteMessageError", "يمكنك حذف الرساله الخاصه بك فقط");
            return;
        }

        unitOfWork.ChatRepo.Remove(chatMessage);

        await unitOfWork.SaveAsync();

        await Clients.Users([chatMessage.SenderId, chatMessage.ReceiverId])
            .SendAsync("MessageDeleted", messageId);
    }
}
