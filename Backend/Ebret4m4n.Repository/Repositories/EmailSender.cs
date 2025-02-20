using Ebret4m4n.Contracts;
using Ebret4m4n.Repository.Configuration;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Ebret4m4n.Repository.Repositories;

public class EmailSender(IOptions<EmailSettings> emailSettings) : IEmailSender
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;

    public async Task SendEmailAsync(string email, string subject, string body)
    {
        using (var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort))
        {
            client.Credentials =
                new NetworkCredential(_emailSettings.Username, _emailSettings.Password);

            client.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.Username, "EbretAman"), // ✅ Custom sender name
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(new MailAddress(email));

            await client.SendMailAsync(mailMessage);
        }
    }
}
