namespace Ebret4m4n.Contracts;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}
