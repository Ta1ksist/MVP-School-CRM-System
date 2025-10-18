using CRM.Core.Abstractions.Repositories;
using CRM.Core.Abstractions.Services;
using CRM.Core.Models;

namespace CRM.Application.Services;

public class ClubPaymentService : IClubPaymentService
{
    private readonly IClubPaymentRepository _clubPaymentRepository;

    public ClubPaymentService(IClubPaymentRepository clubPaymentRepository)
    {
        _clubPaymentRepository = clubPaymentRepository;
    }

    public async Task<ClubPayment> GetPaymentsByEnrollmentId(Guid enrollmentId)
    {
        return await _clubPaymentRepository.GetPaymentsByEnrollmentId(enrollmentId);
    }

    public async Task<Guid> AddClubPayment(ClubPayment payment)
    {
        return await _clubPaymentRepository.AddClubPayment(payment);
    }
}