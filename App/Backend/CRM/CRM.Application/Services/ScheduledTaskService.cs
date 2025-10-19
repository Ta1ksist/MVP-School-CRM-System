using CRM.Core.Abstractions.Services;

namespace CRM.Application.Services;

public class ScheduledTasksService : IScheduledTasksService
{
    private readonly IReportService _reportService;
    private readonly IEmailService _emailService;

    public ScheduledTasksService(IReportService reportService, IEmailService emailService)
    {
        _reportService = reportService;
        _emailService = emailService;
    }

    public async Task SendMonthlyIncomeReport()
    {
        var now = DateTime.Now;
        var startDate = new DateTime(now.Year, now.Month - 1, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        var pdf = await _reportService.GenerateIncomeReportPdf(startDate, endDate);

        await _emailService.SendIncomeReport(
            toEmail: "director@school.com",
            subject: "Финансовый отчет по кружкам",
            body: $"Здравствуйте! В приложении — финансовый отчет за {startDate:MMMM yyyy}.",
            pdfAttachment: pdf,
            fileName: $"income_report_{startDate:yyyyMM}.pdf"
        );
    }
}
