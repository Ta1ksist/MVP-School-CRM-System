using CRM.Core.DTOs;

namespace CRM.Core.Abstractions.Services;

public interface IReportService
{
    Task<byte[]> GenerateDebtReportExcel(IEnumerable<Guid> pupilIds);
    Task<byte[]> GenerateDebtReportPdf(IEnumerable<PupilDebDTO> debts);
}