using CRM.Core.Models;

namespace CRM.Core.Abstractions.Services;

public interface IClubPaymentService
{
    Task<ClubPayment> GetPaymentsByEnrollmentId(Guid enrollmentId);
    Task<Guid> AddClubPayment(ClubPayment payment);
}