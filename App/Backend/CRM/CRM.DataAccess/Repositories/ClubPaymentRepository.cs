using AutoMapper;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.Models;
using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DataAccess.Repositories;

public class ClubPaymentRepository : IClubPaymentRepository
{
    private readonly CRMContext _context;
    private readonly IMapper _mapper;
    
    public ClubPaymentRepository(CRMContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ClubPayment> GetPaymentsByEnrollmentId(Guid enrollmentId)
    {
        var clubPaymentEntity = await _context.ClubPayment
            .Where(c => c.EnrollmentId == enrollmentId)
            .FirstOrDefaultAsync();
        var clubPayment = _mapper.Map<ClubPayment>(clubPaymentEntity);
        return clubPayment;
    }

    public async Task<Guid> AddClubPayment(ClubPayment payment)
    {
        var clubPaymentEntity = _mapper.Map<ClubPaymentEntity>(payment);
        
        await _context.ClubPayment.AddAsync(clubPaymentEntity);
        await _context.SaveChangesAsync();
        
        return clubPaymentEntity.Id;
    }
}