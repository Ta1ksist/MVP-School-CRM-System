using CRM.Core.Models;

namespace CRM.API.Contracts.Responses;

public record ClubResponse(
    Guid Id,
    string Name,
    string Description,
    decimal MonthlyFee,
    bool IsActive,
    ClubEnrollment Enrollments);