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

    public async Task<List<string>> GetEmailsAllPupils()
    {
        var pupil = await _context.Pupils
            .Include(p => p.Parents)
            .Where(p => !string.IsNullOrEmpty(p.Email))
            .AsNoTracking()
            .Select(p => p.Email)
            .ToListAsync();
        var emails = _mapper.Map<List<string>>(pupil);
        return emails;
    }
    
    public async Task<Guid> AddPupil(Pupil pupil)
    {
        var pupilEntity = new PupilEntity
        {
            Id = Guid.NewGuid(),
            FirstName = pupil.FirstName,
            LastName = pupil.LastName,
            DateOfBirth = pupil.DateOfBirth,
            GradeId = pupil.GradeId,
            PhoneNumber = pupil.PhoneNumber,
            Email = pupil.Email,
            Address = pupil.Address
        };
        await _context.AddAsync(pupilEntity);
        await _context.SaveChangesAsync();
        
        return pupilEntity.Id;
    }

    public async Task AddParentToPupil(Guid pupilId, Guid parentId)
    {
        var pu = await _context.Pupils
            .Include(p => p.Parents)
            .FirstOrDefaultAsync(p => p.Id == pupilId);
        if (pu == null) throw new KeyNotFoundException("Ученик не найден");

        var pa = await _context.Parents.FirstOrDefaultAsync(p => p.Id == parentId);
        if (pa == null) throw new KeyNotFoundException("Родитель не найден");

        pu.Id = pupilId;
        if (!pu.Parents.Any(p => p.Id == parentId)) pu.Parents.Add(pa);

        await _context.SaveChangesAsync();
    }
    
    public async Task<Guid> UpdatePupil(Guid id, string firstName, string lastName, string patronymic,
        DateOnly dateOfBirth,
        Guid gradeId,string phoneNumber, string email, string address)
    {
        var pupilEntity = await _context.Pupils
            .Include(p => p.Parents)
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