namespace CRM.Core.Abstractions.Services;

public interface IEmailService
{
    Task SendDebtNotification(string toEmail, string pupilFirstName, string pupilLastName, decimal debt);
}