using CRM.Core.Abstractions.Repositories;
using CRM.Core.Abstractions.Services;
using CRM.Core.DTOs;
using OfficeOpenXml;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace CRM.Application.Services;

public class ReportService : IReportService
{
    private readonly IClubEnrollmentService _clubEnrollmentService;

    public ReportService(IClubEnrollmentService clubEnrollmentService)
    {
        _clubEnrollmentService = clubEnrollmentService;
    }

    public async Task<byte[]> GenerateDebtReportExcel(IEnumerable<Guid> pupilIds)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("Отчет о долге");

        worksheet.Cells[1, 1].Value = "ID ученика";
        worksheet.Cells[1, 2].Value = "Имя ученика";
        worksheet.Cells[1, 3].Value = "Фамилия ученика";
        worksheet.Cells[1, 4].Value = "Класс";
        worksheet.Cells[1, 5].Value = "Всего ожидается";
        worksheet.Cells[1, 6].Value = "Всего оплачено";
        worksheet.Cells[1, 7].Value = "Долг";

        int row = 2;
        foreach (var pupilId in pupilIds)
        {
            var debt = await _clubEnrollmentService.GetPupilDebt(pupilId);

            worksheet.Cells[row, 1].Value = pupilId.ToString();
            worksheet.Cells[row, 2].Value = debt.PupilFirstName;
            worksheet.Cells[row, 3].Value = debt.PupilLastName;
            worksheet.Cells[row, 4].Value = debt.PupilGrade;
            worksheet.Cells[row, 5].Value = debt.TotalExpectedAmount;
            worksheet.Cells[row, 6].Value = debt.TotalPaidAmount;
            worksheet.Cells[row, 7].Value = debt.Debt;

            row++;
        }

        return package.GetAsByteArray();
    }

    public async Task<byte[]> GenerateDebtReportPdf(IEnumerable<PupilDebDTO> debts)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                    .Text("Отчёт по задолженностям")
                    .SemiBold().FontSize(18).FontColor(Colors.Blue.Medium);

                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(40);
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.ConstantColumn(100);
                        columns.ConstantColumn(100);
                        columns.ConstantColumn(100);
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("#").Bold();
                        header.Cell().Text("ID ученика").Bold();
                        header.Cell().Text("Имя ученика").Bold();
                        header.Cell().Text("Фамилия ученика").Bold();
                        header.Cell().Text("Класс ученика").Bold();
                        header.Cell().Text("Ожидается").Bold();
                        header.Cell().Text("Оплачено").Bold();
                        header.Cell().Text("Долг").Bold();
                    });

                    int index = 1;
                    foreach (var d in debts)
                    {
                        table.Cell().Text(index++);
                        table.Cell().Text(d.PupilId.ToString());
                        table.Cell().Text(d.PupilFirstName);
                        table.Cell().Text(d.PupilLastName);
                        table.Cell().Text(d.PupilGrade);
                        table.Cell().Text($"{d.TotalExpectedAmount} ₽");
                        table.Cell().Text($"{d.TotalPaidAmount} ₽");
                        table.Cell().Text($"{d.Debt} ₽");
                    }
                });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Дата генерации: ");
                        x.Span(DateTime.Now.ToString("dd.MM.yyyy HH:mm")).SemiBold();
                    });
            });
        });

        return document.GeneratePdf();
    }
    
    public async Task<byte[]> GenerateIncomeReportExcel(DateTime startDate, DateTime endDate)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("Отчет о доходах");

        worksheet.Cells[1, 1].Value = "Название кружка";
        worksheet.Cells[1, 2].Value = "Месяц";
        worksheet.Cells[1, 3].Value = "Общий доход (₽)";

        var enrollments = await _clubEnrollmentService.GetAllWithPayments();
        var allPayments = enrollments
            .SelectMany(e => e.Payments)
            .Where(p => p.PaymentDate.Date >= startDate.Date && p.PaymentDate.Date <= endDate.Date)
            .ToList();

        var grouped = allPayments
            .GroupBy(p => new
            {
                ClubName = p.Enrollment.Club.Name,
                Month = new DateTime(p.PaymentDate.Year, p.PaymentDate.Month, 1)
            })
            .Select(g => new
            {
                g.Key.ClubName,
                g.Key.Month,
                Total = g.Sum(x => x.Amount)
            })
            .OrderBy(x => x.Month)
            .ThenBy(x => x.ClubName)
            .ToList();

        int row = 2;
        foreach (var item in grouped)
        {
            worksheet.Cells[row, 1].Value = item.ClubName;
            worksheet.Cells[row, 2].Value = item.Month.ToString("MMMM yyyy");
            worksheet.Cells[row, 3].Value = item.Total;

            row++;
        }

        worksheet.Cells[row + 1, 2].Value = "ИТОГО:";
        worksheet.Cells[row + 1, 3].Formula = $"SUM(C2:C{row - 1})";
        worksheet.Cells[row + 1, 3].Style.Font.Bold = true;

        return package.GetAsByteArray();
    }
    
    public async Task<byte[]> GenerateIncomeReportPdf(DateTime startDate, DateTime endDate)
    {
        var enrollments = await _clubEnrollmentService.GetAllWithPayments();

        var payments = enrollments
            .SelectMany(e => e.Payments)
            .Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate)
            .ToList();

        var grouped = payments
            .GroupBy(p => new
            {
                p.Enrollment.Club.Name,
                Year = p.PaymentDate.Year,
                Month = p.PaymentDate.Month
            })
            .Select(g => new MonthlyClubIncomeDTO
            {
                ClubName = g.Key.Name,
                Year = g.Key.Year,
                Month = g.Key.Month,
                TotalIncome = g.Sum(p => p.Amount)
        })
            .OrderBy(x => x.Year)
            .ThenBy(x => x.Month)
            .ThenBy(x => x.ClubName)
            .ToList();
        
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                    .Text("Финансовый отчет по кружкам")
                    .SemiBold().FontSize(18).FontColor(Colors.Green.Medium);

                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(40);
                        columns.RelativeColumn();
                        columns.ConstantColumn(80);
                        columns.ConstantColumn(80);
                        columns.ConstantColumn(100);
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("#").Bold();
                        header.Cell().Text("Кружок").Bold();
                        header.Cell().Text("Месяц").Bold();
                        header.Cell().Text("Год").Bold();
                        header.Cell().Text("Доход").Bold();
                    });

                    int index = 1;
                    foreach (var item in grouped)
                    {
                        table.Cell().Text(index++);
                        table.Cell().Text(item.ClubName);
                        table.Cell().Text(System.Globalization.CultureInfo.GetCultureInfo("ru-RU")
                            .DateTimeFormat.GetMonthName(item.Month));
                        table.Cell().Text(item.Year.ToString());
                        table.Cell().Text($"{item.TotalIncome} ₽");
                    }
                });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Дата генерации: ");
                        x.Span(DateTime.Now.ToString("dd.MM.yyyy HH:mm")).SemiBold();
                    });
            });
        });

        return document.GeneratePdf();
    }

}