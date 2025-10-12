using AutoMapper;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.Models;
using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DataAccess.Repositories;

public class ParentRepository : IParentRepository
{
    private readonly CRMContext _context;
    private readonly IMapper _mapper;
    
    public ParentRepository(CRMContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Parent>> GetAllParents()
    {
        var parentEntity = await _context.Parents
            .Include(t => t.Pupil)
            .AsNoTracking()
            .ToListAsync();
        
        var parent = _mapper.Map<List<Parent>>(parentEntity);
        return parent;
    }

    public async Task<Guid> AddParent(Parent parent)
    {
        var parentEntity = _mapper.Map<ParentEntity>(parent);

        await _context.Parents.AddAsync(parentEntity);
        await _context.SaveChangesAsync();
        
        return parentEntity.Id;
    }

    public async Task<Guid> UpdateParent(Guid id, string firstName, string lastName, string patronymic,
        DateOnly dateOfBirth,
        string role, string phoneNumber, string email, string address, Guid pupilId, ICollection<Pupil> pupil)
    {
        var parentEntity = await _context.Parents
            .Include(g => g.Pupil)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (parentEntity == null)
            throw new Exception("Родитель не найден");

        var pupilEntity = await _context.Pupils.FirstOrDefaultAsync(p => p.Id == pupilId);
        if (pupilEntity == null)
            throw new Exception("Ученик не найден");

        parentEntity.FirstName = firstName;
        parentEntity.LastName = lastName;
        parentEntity.Patronymic = patronymic;
        parentEntity.DateOfBirth = dateOfBirth;
        parentEntity.Role = role;
        parentEntity.PhoneNumber = phoneNumber;
        parentEntity.Email = email;
        parentEntity.Address = address;
        parentEntity.Pupil = pupilEntity;
        parentEntity.PupilId = pupilId;

        await _context.SaveChangesAsync();
        return id;
    }

    public async Task<Guid> DeleteParent(Guid id)
    {
        var parentEntity = await _context.Parents.Where(p => p.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}