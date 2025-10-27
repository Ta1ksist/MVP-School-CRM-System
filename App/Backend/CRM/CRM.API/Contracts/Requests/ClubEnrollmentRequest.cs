using CRM.Core.Models;

namespace CRM.API.Contracts.Requests;

public record ClubEnrollmentRequest(
    Guid ClubId,
    Club Club,
    Guid PupilId,
    DateTime EnrollmentDate,
    bool IsActive,
    ICollection<ClubPayment> Payments);