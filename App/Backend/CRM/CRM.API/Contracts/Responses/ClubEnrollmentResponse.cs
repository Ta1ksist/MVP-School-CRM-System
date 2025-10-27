using CRM.Core.Models;

namespace CRM.API.Contracts.Responses;

public record ClubEnrollmentResponse(
    Guid Id,
    Guid ClubId,
    Club Club,
    Guid PupilId,
    DateTime EnrollmentDate,
    bool IsActive,
    ICollection<ClubPayment> Payments);