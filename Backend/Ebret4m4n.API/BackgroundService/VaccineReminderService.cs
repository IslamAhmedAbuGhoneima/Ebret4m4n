using Ebret4m4n.Shared.DTOs.SignalRDtos;
using Microsoft.AspNetCore.SignalR;
using Ebret4m4n.API.Utilites;
using Ebret4m4n.Contracts;
using Ebret4m4n.API.Hubs;
using Mapster;

namespace Ebret4m4n.API.BackgroundService;

public class VaccineReminderService(IUnitOfWork unitOfWork, 
    IHubContext<NotificationHub> hubContext)
{
    public async Task CheckVaccineRemindersAndSendNotificationsAsync()
     {
        await unitOfWork.BeginTransactionAsync();
        try
        {
            var childern = unitOfWork.ChildRepo.FindAll(false, ["User", "Vaccines"])
                .ToList() ?? [];

            if(childern.Count == 0)
                return;

            foreach(var child in childern)
            {
                if(child.Vaccines == null || child.Vaccines.Count == 0)
                    continue;

                var vaccine = child.Vaccines.FirstOrDefault(vaccine => vaccine.ChildAge == child.AgeInMonth);

                if (vaccine == null || vaccine.IsTaken == true) 
                    continue;

                var message = $"لتلقي التطعيم {vaccine.Name} تم فتح حجز اللقاحات الرجاء الحجز والتوجه للوحده الصحيه التابع لها  {child.Name} لطفلك";

                var notification = Utility.CreateNotification("تنبيه لاقتراب موعد الطعيم", message, child.UserId);

                await hubContext.Clients.User(child.UserId).SendAsync("NotificationMessage", notification.Adapt<NotificationDto>());

                await unitOfWork.NotificationRepo.AddAsync(notification);
            }

            await unitOfWork.SaveAsync();

            await unitOfWork.CommitTransactionAsync();
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
