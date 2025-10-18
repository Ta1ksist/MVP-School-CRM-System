using AutoMapper;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.Models;
using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DataAccess.Repositories;

public class PupilRepository  : IPupilRepository
{
    private readonly CRMContext _context;
    private readonly IMapper _mapper;
    
    public PupilRepository(CRMContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Pupil> GetPupilByName(string firstName, string lastName)
    {
        var pupilEntity = await _context.Pupils
            .Where(p => p.FirstName == firstName && p.LastName == lastName)
            .FirstOrDefaultAsync();
        var pupil = _mapper.Map<Pupil>(pupilEntity);
        
        return pupil;
    }
    
    public async Task<List<Pupil>> GetAllPupils()
    {
        var pupilEntity = await _context.Pupils
            .Include(p => p.Parents)
            .AsNoTracking()
            .ToListAsync();
        
        var pupils = _mapper.Map<List<Pupil>>(pupilEntity).ToList();
        return pupils;
    }

    public async Task<Guid> AddPupil(Pupil pupil)
    {
        var pupilEntity = _mapper.Map<PupilEntity>(pupil);

        await _context.AddAsync(pupilEntity);
        await _context.SaveChangesAsync();
        
        return pupilEntity.Id;
    }

    public async Task<Guid> UpdatePupil(Guid id, string firstName, string lastName, string patronymic,
        DateOnly dateOfBirth,
        Guid gradeId, Grade grade, string phoneNumber, string email, string address, ICollection<Parent> parents)
    {
        var pupilEntity = await _context.Pupils
            .Include(p => p.Parents)
            .Include(p => p.Grade)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pupilEntity == null) throw new Exception("Ученик не найден");

        pupilEntity.FirstName = firstName;
        pupilEntity.LastName = lastName;
        pupilEntity.Patronymic = patronymic;
        pupilEntity.DateOfBirth = dateOfBirth;
        pupilEntity.GradeId = gradeId;
        pupilEntity.PhoneNumber = phoneNumber;
        pupilEntity.Email = email;
        pupilEntity.Address = address;

        _context.Parents.RemoveRange(pupilEntity.Parents);

        var parentEntities = parents.Select(p => _mapper.Map<ParentEntity>(p)).ToList();
        pupilEntity.Parents = parentEntities;

        await _context.SaveChangesAsync();

        return id;
    }

    public async Task<Guid> DeletePupil(Guid id)
    {
        var pupilEntity = await _context.Pupils
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
        
        return id;
    }
}