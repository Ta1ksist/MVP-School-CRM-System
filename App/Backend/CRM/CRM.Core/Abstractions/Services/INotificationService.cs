using CRM.Core.Models;

namespace CRM.Core.Abstractions.Services;

public interface INotificationService
{
    Task SendDebtNotification(string toEmail, string pupilFirstName, string pupilLastName, decimal debt);
    Task SendEventNotification(IEnumerable<string> emails, Event eevent);
    Task SendAboutEventChangeNotification(IEnumerable<string> emails, string message);
    Task SendNewsNotification(IEnumerable<string> emails, News news);

    Task SendIncomeReport(string toEmail, string subject, string body, byte[] pdfAttachment,
        string fileName);
}