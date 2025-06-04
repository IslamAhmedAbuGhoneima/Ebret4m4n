using Hangfire;

namespace Ebret4m4n.API.BackgroundService;

public static class ScheduleJob
{
    public static void NotificationMessagJob(this WebApplication app)
    {
        RecurringJob.AddOrUpdate<ReservationReminderService>("reservation-reminders",
            service => service.CheckReservationsAndSendNotificationsAsync(),
            Cron.Daily());

        RecurringJob.AddOrUpdate<VaccineReminderService>("vaccine-reminders",
            service => service.CheckVaccineRemindersAndSendNotificationsAsync(),
            Cron.Weekly(DayOfWeek.Tuesday));
    }
 
}
