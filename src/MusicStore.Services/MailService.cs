using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MusicStore.Entities;
using System.Net.Mail;

namespace MusicStore.Services;

public class EmailService : IEmailService
{
    private readonly IOptions<AppSettings> options;
    private readonly ILogger<EmailService> logger;

    public EmailService(IOptions<AppSettings> options, ILogger<EmailService> logger)
    {
        this.options = options;
        this.logger = logger;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var smtp = options.Value.Smtp;
        var mailMessage = new MailMessage(new MailAddress(smtp.UserName, smtp.FromName),
            new MailAddress(email));

        mailMessage.Subject = subject;
        mailMessage.Body = message;
        mailMessage.IsBodyHtml = true;

        using var smtpClient = new SmtpClient(smtp.Server, smtp.Port)
        {
            Credentials = new System.Net.NetworkCredential(smtp.UserName, smtp.Password),
            EnableSsl = smtp.EnableSsl,
            UseDefaultCredentials = false
        };

        try
        {
            await smtpClient.SendMailAsync(mailMessage);
            logger.LogInformation("Email sent to {Email}", email);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send email to {Email}: {Message}", email, ex.Message);
            throw;
        }
    }
}
