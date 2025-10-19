using CRM.Core.Abstractions.Services;
using CRM.Core.Models;

namespace CRM.Application.Services;

public class NotificationService : INotificationService
{
    private readonly IEmailService _emailService;
    
    public NotificationService(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task SendDebtNotification(string toEmail, string pupilFirstName, string pupilLastName, decimal debt)
    {
        await _emailService.SendDebtNotification(toEmail, pupilFirstName, pupilLastName, debt);
    }

    public async Task SendEventNotification(IEnumerable<string> emails, Event eevent)
    {
        await _emailService.SendEventNotification(emails, eevent);
    }

    public async Task SendAboutEventChangeNotification(IEnumerable<string> emails, string message)
    {
        await _emailService.SendAboutEventChangeNotification(emails, message);
    }

    public async Task SendNewsNotification(IEnumerable<string> emails, News news)
    {
        await _emailService.SendNewsNotification(emails, news);
    }

    public async Task SendIncomeReport(string toEmail, string subject, string body, byte[] pdfAttachment,
        string fileName)
    {
        await _emailService.SendIncomeReport(toEmail, subject, body, pdfAttachment, fileName);
    }
}