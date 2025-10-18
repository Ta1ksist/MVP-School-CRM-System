using CRM.Core.Models;

namespace CRM.Core.Abstractions.Repositories;

public interface IClubPaymentRepository
{
    Task<ClubPayment> GetPaymentsByEnrollmentId(Guid enrollmentId);
    Task<Guid> AddClubPayment(ClubPayment payment);
}