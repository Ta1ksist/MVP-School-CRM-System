using CRM.Application.Settings;
using CRM.Core.Abstractions.Services;
using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;

namespace CRM.Application.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task SendDebtNotification(string toEmail, string pupilFirstName, string pupilLastName, decimal debt)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_settings.FromEmail));
        message.To.Add(MailboxAddress.Parse(toEmail));
        message.Subject = "Задолженность по кружкам";

        var bodyBuilder = new BodyBuilder();
        bodyBuilder.TextBody = $"Здравствуйте, {pupilFirstName} {pupilLastName}. У вас задолженность по кружкам: {debt:C2}. Пожалуйста, оплатите.";

        message.Body = bodyBuilder.ToMessageBody();

        using var smtp = new SmtpClient();
        
        try
        {
            await smtp.ConnectAsync(_settings.SmtpHost, _settings.SmtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_settings.FromEmail, _settings.Password);
            await smtp.SendAsync(message);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Ошибка отправки email: {ex.Message}", ex);
        }
        finally
        {
            await smtp.DisconnectAsync(true);
        }
    }
}