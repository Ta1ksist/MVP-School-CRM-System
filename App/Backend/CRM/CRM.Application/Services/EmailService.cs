using CRM.Application.Settings;
using CRM.Core.Abstractions.Services;
using CRM.Core.Models;
using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;

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

    public async Task SendEventNotification(IEnumerable<string> emails, Event eevent)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_settings.FromEmail));
        message.Subject = "Долгожданное событие";
        
        var bodyBuilder = new BodyBuilder();

        bodyBuilder.TextBody = $"Совсем скоро: {eevent.Date} состоится мероприятие: {eevent.Name}. {eevent.Description}";
        message.Body = bodyBuilder.ToMessageBody();
        
        using var smtp = new SmtpClient();

        try
        {
            await smtp.ConnectAsync(_settings.SmtpHost, _settings.SmtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_settings.FromEmail, _settings.Password);
            foreach (var email in emails)
            {
                message.To.Clear();
                message.To.Add(MailboxAddress.Parse(email));
                await smtp.SendAsync(message);
            }
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
    
    public async Task SendAboutEventChangeNotification(IEnumerable<string> emails, string message)
    {
        var mimeMessage = new MimeMessage();
        mimeMessage.From.Add(MailboxAddress.Parse(_settings.FromEmail));
        mimeMessage.Subject = "Изменение События/мероприятия";
        
        var bodyBuilder = new BodyBuilder();

        bodyBuilder.TextBody = message;
        mimeMessage.Body = bodyBuilder.ToMessageBody();
        
        using var smtp = new SmtpClient();

        try
        {
            await smtp.ConnectAsync(_settings.SmtpHost, _settings.SmtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_settings.FromEmail, _settings.Password);
            foreach (var email in emails)
            {
                mimeMessage.To.Clear();
                mimeMessage.To.Add(MailboxAddress.Parse(email));
                await smtp.SendAsync(mimeMessage);
            }
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

    public async Task SendNewsNotification(IEnumerable<string> emails, News news)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_settings.FromEmail));
        message.Subject = "Наши новости!";
        
        var bodyBuilder = new BodyBuilder();
        
        bodyBuilder.TextBody = $"{news.Title} {news.Description} {news.Date}";
        message.Body = bodyBuilder.ToMessageBody();
        
        using var smtp = new SmtpClient();

        try
        {
            await smtp.ConnectAsync(_settings.SmtpHost, _settings.SmtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_settings.FromEmail, _settings.Password);
            foreach (var email in emails)
            {
                message.To.Clear();
                message.To.Add(MailboxAddress.Parse(email));
                await smtp.SendAsync(message);
            }
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
    
    public async Task SendIncomeReport(string toEmail, string subject, string body, byte[] pdfAttachment, string fileName)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_settings.FromEmail));
        message.To.Add(MailboxAddress.Parse(toEmail));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder
        {
            TextBody = body
        };

        if (pdfAttachment != null && pdfAttachment.Length > 0)
        {
            bodyBuilder.Attachments.Add(fileName, pdfAttachment, new ContentType("application", "pdf"));
        }

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
            throw new InvalidOperationException($"Ошибка отправки отчета по email: {ex.Message}", ex);
        }
        finally
        {
            await smtp.DisconnectAsync(true);
        }
    }

}