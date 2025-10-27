using CRM.Core.Abstractions.Services;
using CRM.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FinanceController : ControllerBase
{
    private readonly IFinancialReportService  _financialReportService;
    private readonly IClubEnrollmentService _clubEnrollmentService;
    private readonly IScheduledTasksService  _scheduledTasksService;

    public FinanceController(IFinancialReportService financialReportService, IClubEnrollmentService clubEnrollmentService, 
        IScheduledTasksService  scheduledTasksService)
    {
        _financialReportService = financialReportService;
        _clubEnrollmentService = clubEnrollmentService;
        _scheduledTasksService = scheduledTasksService;
    }
    
    [HttpGet("mothlyReport")]
    public async Task<ActionResult> SendMonthlyIncomeReport()
    {
        await _scheduledTasksService.SendMonthlyIncomeReport();
        return Ok();
    }
    
    [HttpPost("debt/excel")]
    public async Task<IActionResult> GenerateExcel([FromBody] List<Guid> pupilIds)
    {
        var bytes = await _financialReportService.GenerateDebtReportExcel(pupilIds);
        return File(bytes,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DebtReport.xlsx");
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
        var pdfBytes = await _financialReportService.GenerateDebtReportPdf(debts);
        
        return File(pdfBytes, "application/pdf", "DebtReport.pdf");
    }
}