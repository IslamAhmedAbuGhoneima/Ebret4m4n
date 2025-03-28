using Ebret4m4n.Shared.DTOs.SignalRDtos;
using Ebret4m4n.Entities.Exceptions;
using Microsoft.AspNetCore.SignalR;
using Ebret4m4n.Entities.Models;
using Ebret4m4n.API.Utilites;
using Ebret4m4n.Contracts;
using Ebret4m4n.API.Hubs;
using Mapster;

namespace Ebret4m4n.API.BackgroundService;

public class ReservationReminderService
    (IUnitOfWork unitOfWork,IHubContext<NotificationHub> hubContext)
{

    public async Task CheckReservationsAndSendNotificationsAsync()
    {
        await unitOfWork.BeginTransactionAsync();

        try
        {
            var targetDate = DateTime.UtcNow.AddDays(1).Date;

            var usersWithReservations =
                unitOfWork.AppointmentRepo.FindByCondition(appointment => appointment.Date.Date == targetDate, false, ["Child", "HealthCareCenter"])
                .Select(appointment => new { appointment.UserId, ChildName = appointment.Child.Name, appointment.VaccineName, appointment.HealthCareCenter.HealthCareCenterName })
                .ToList();

            if (usersWithReservations.Count == 0) 
                return;

            List<Notification> notifications = [];

            foreach (var user in usersWithReservations)
            {
                var message = $"لتلقي التطعيم {user.HealthCareCenterName} الرجاء التوجه للوحده الصحيه {user.ChildName} لطفلك {user.VaccineName} ننبهك ان غدا موعد الطعيم للجرعه";
                var notification = Utility.CreateNotification("تنبيه لاقتراب الحجز", message, user.UserId);

                notifications.Add(notification);

                var notificationDto = notification.Adapt<NotificationDto>();

                await hubContext.Clients.User(user.UserId).SendAsync("Notification Message", notificationDto);
            }

            await unitOfWork.NotificationRepo.AddRangeAsync(notifications);
            var result = await unitOfWork.SaveAsync();

            if (result == 0)
                throw new BadRequestException("لم يتم حفظ التنبيهات");

            await unitOfWork.CommitTransactionAsync();
        }
        catch(Exception)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
