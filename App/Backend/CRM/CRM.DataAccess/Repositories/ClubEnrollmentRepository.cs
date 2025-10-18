using AutoMapper;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.Models;
using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DataAccess.Repositories;

public class ClubEnrollmentRepository  : IClubEnrollmentRepository
{
    private readonly CRMContext _context;
    private readonly IMapper _mapper;

    public ClubEnrollmentRepository(CRMContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ClubEnrollment> GetClubEnrollmentById(Guid id)
    {
        var clubEnrollmentEntity = await _context.ClubEnrollments
            .Include(e => e.Payments)
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
        var clubEnrollment = _mapper.Map<ClubEnrollment>(clubEnrollmentEntity);
        return clubEnrollment;
    }

    public async Task<List<ClubEnrollment>> GetClubEnrollmentByPupilId(Guid pupilId)
    {
        var clubEnrollmentEntities = await _context.ClubEnrollments
            .Include(e => e.Payments)
            .Include(e => e.Club)
            .Where(c => c.PupilId == pupilId)
            .ToListAsync();
    
        var clubEnrollments = _mapper.Map<List<ClubEnrollment>>(clubEnrollmentEntities);
        return clubEnrollments;
    }

    public async Task<Guid> AddClubEnrollment(ClubEnrollment enrollment)
    {
        var clubEnrollmentEntity = _mapper.Map<ClubEnrollmentEntity>(enrollment);
        
        await _context.AddAsync(clubEnrollmentEntity);
        await _context.SaveChangesAsync();
        
        return clubEnrollmentEntity.Id;
    }

    public async Task<Guid> UpdateClubEnrollment(ClubEnrollment enrollment)
    {
        var existing = await _context.ClubEnrollments.FindAsync(enrollment.Id);

        if (existing == null) throw new Exception("Запись ученика не найдена");

        existing.ClubId = enrollment.ClubId;
        existing.PupilId = enrollment.PupilId;
        existing.EnrollmentDate = enrollment.EnrollmentDate;
        existing.IsActive = enrollment.IsActive;


        _context.ClubEnrollments.Update(existing);
        await _context.SaveChangesAsync();
        
        return existing.Id;
    }
}