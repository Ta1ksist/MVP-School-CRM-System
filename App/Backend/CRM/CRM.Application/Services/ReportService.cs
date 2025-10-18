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
        var worksheet = package.Workbook.Worksheets.Add("Debt Report");

        worksheet.Cells[1, 1].Value = "Pupil ID";
        worksheet.Cells[1, 2].Value = "Total Expected";
        worksheet.Cells[1, 3].Value = "Total Paid";
        worksheet.Cells[1, 4].Value = "Debt";

        int row = 2;
        foreach (var pupilId in pupilIds)
        {
            var debt = await _clubEnrollmentService.GetPupilDebt(pupilId);

            worksheet.Cells[row, 1].Value = pupilId.ToString();
            worksheet.Cells[row, 2].Value = debt.TotalExpectedAmount;
            worksheet.Cells[row, 3].Value = debt.TotalPaidAmount;
            worksheet.Cells[row, 4].Value = debt.Debt;

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
                        columns.ConstantColumn(100);
                        columns.ConstantColumn(100);
                        columns.ConstantColumn(100);
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("#").Bold();
                        header.Cell().Text("Student ID").Bold();
                        header.Cell().Text("Ожидается").Bold();
                        header.Cell().Text("Оплачено").Bold();
                        header.Cell().Text("Долг").Bold();
                    });

                    int index = 1;
                    foreach (var d in debts)
                    {
                        table.Cell().Text(index++);
                        table.Cell().Text(d.PupilId.ToString());
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
}