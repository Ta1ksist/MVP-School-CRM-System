using CRM.Core.Models;

namespace CRM.API.Contracts.Requests;

public record ClubPaymentRequest(
    Guid EnrollmentId,
    ClubEnrollment Enrollment,
    DateTime PaymentDate,
    decimal Amount,
    string PaymentMethod);