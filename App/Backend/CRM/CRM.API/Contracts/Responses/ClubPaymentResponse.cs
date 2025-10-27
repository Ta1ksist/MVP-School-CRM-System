using CRM.Core.Models;

namespace CRM.API.Contracts.Responses;

public record ClubPaymentResponse(
    Guid Id,
    Guid EnrollmentId,
    ClubEnrollment Enrollment,
    DateTime PaymentDate,
    decimal Amount,
    string PaymentMethod);