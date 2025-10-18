using CRM.Application.Services;
using CRM.Core.Abstractions.Services;
using CRM.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;
    private readonly IClubEnrollmentService _clubEnrollmentService;

    public ReportController(IReportService reportService, IClubEnrollmentService clubEnrollmentService)
    {
        _reportService = reportService;
        _clubEnrollmentService = clubEnrollmentService;
    }
    
    [HttpPost("debt/excel")]
    public async Task<IActionResult> GenerateExcel([FromBody] List<Guid> pupilIds)
    {
        var bytes = await _reportService.GenerateDebtReportExcel(pupilIds);
        return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DebtReport.xlsx");
    }

    [HttpPost("debt/excel")]
    public async Task<IActionResult> GeneratePdf([FromBody] List<Guid> pupilIds)
    {
        var debts = new List<PupilDebDTO>();

        foreach (var pupilId in pupilIds)
        {
            var debt = await _clubEnrollmentService.GetPupilDebt(pupilId);
            debts.Add(debt);
        }

        var pdfBytes = await _reportService.GenerateDebtReportPdf(debts);
        return File(pdfBytes, "application/pdf", "DebtReport.pdf");
    }
}