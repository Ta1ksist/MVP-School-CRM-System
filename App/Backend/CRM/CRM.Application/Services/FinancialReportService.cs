using CRM.Core.Abstractions.Services;
using CRM.Core.DTOs;

namespace CRM.Application.Services;

public class FinancialReportService : IFinancialReportService
{
    private readonly IReportService _reportService;
    
    public FinancialReportService(IReportService reportService)
    {
        _reportService = reportService;
    }

    public async Task<byte[]> GenerateDebtReportExcel(IEnumerable<Guid> pupilIds)
    {
        return await _reportService.GenerateDebtReportExcel(pupilIds);
    }

    public async Task<byte[]> GenerateDebtReportPdf(IEnumerable<PupilDebDTO> debts)
    {
        return await _reportService.GenerateDebtReportPdf(debts);
    }

    public async Task<byte[]> GenerateIncomeReportExcel(DateTime startDate, DateTime endDate)
    {
        return await _reportService.GenerateIncomeReportExcel(startDate, endDate);
    }

    public async Task<byte[]> GenerateIncomeReportPdf(DateTime startDate, DateTime endDate)
    {
        return await _reportService.GenerateIncomeReportPdf(startDate, endDate);
    }
}