using CRM.Core.Models;

namespace CRM.API.Contracts.Requests;

public record ClubRequest(
    string Name,
    string Description,
    decimal MonthlyFee,
    bool IsActive,
    ICollection<ClubEnrollment> Enrollments);