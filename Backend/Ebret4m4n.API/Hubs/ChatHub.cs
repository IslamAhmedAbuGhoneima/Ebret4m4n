using Ebret4m4n.Shared.DTOs.SignalRDtos;
using Microsoft.AspNetCore.SignalR;
using Ebret4m4n.Entities.Models;
using Ebret4m4n.Contracts;
using Ebret4m4n.API.Utilites;
using Mapster;
using Ebret4m4n.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ebret4m4n.API.Hubs;

public class ChatHub
    (IUnitOfWork unitOfWork, IHubContext<NotificationHub> hubContext) : Hub
{
    public async Task SendMessage(ChatMessageDto message)
    {
        var chat = message.Adapt<Chat>();
        chat.SentAt = DateTime.UtcNow;

        if (message.File is not null) 
            chat.File = Utility.SaveBase64File(message.File);

        await unitOfWork.ChatRepo.AddAsync(chat);

        string preview;

        if (chat.Message is not null)
            preview = chat.Message.Length > 50 ? $"{chat.Message.Substring(0, 45)}..." : chat.Message;
        else
            preview = "تم ارسال ملف جديد";

        var notification = Utility.CreateNotification("رساله جديده",
            preview
            , chat.ReceiverId);

        await unitOfWork.NotificationRepo.AddAsync(notification);

        await unitOfWork.SaveAsync();

        var chatDto = chat.Adapt<ChatMessageDetailsDto>();

        // Notify the NotificationHub about the new message

        var notificationDto = notification.Adapt<NotificationDto>();

        await hubContext.Clients.User(chat.ReceiverId)
           .SendAsync("NotificationMessage", notificationDto);

        await Clients.Users([chat.SenderId!, chat.ReceiverId!]).SendAsync("ReceiveMessage", chatDto);
    }

    public async Task MarkMessagesAsRead(Guid senderId)
    {
        var receiverId = Context.UserIdentifier;

        // Get the IDs first
        var readMessageIds = await unitOfWork.ChatRepo
            .FindByCondition(m => m.SenderId == senderId.ToString()
                               && m.ReceiverId == receiverId
                               && !m.IsRead, true)
            .Select(m => m.Id)
            .ToListAsync();

        if (readMessageIds.Count == 0)
            return;

        // Now update those by ID
        await unitOfWork.ChatRepo
            .FindByCondition(m => readMessageIds.Contains(m.Id), true)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(m => m.IsRead, true));

        await Clients.User(senderId.ToString()).SendAsync("MessagesMarkedAsRead" , new
        {
            ReceiverId = receiverId,
            MessageIds = readMessageIds
        });
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
