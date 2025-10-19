namespace CRM.Core.Abstractions.Services;

public interface IScheduledTasksService
{
    Task SendMonthlyIncomeReport();
}