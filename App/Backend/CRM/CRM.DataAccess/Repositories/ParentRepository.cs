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

    public async Task<Parent> GetParentByName(string firstName, string lastName)
    {
        var parentEntity = await _context.Parents
            .Where(p => p.FirstName == firstName && p.LastName == lastName)
            .FirstOrDefaultAsync();
        var parent = _mapper.Map<Parent>(parentEntity);
        
        return parent;
    }
    
    public async Task<List<Parent>> GetAllParents()
    {
        var parentEntity = await _context.Parents
            .Include(t => t.Pupils)
            .AsNoTracking()
            .ToListAsync();
        
        var parent = _mapper.Map<List<Parent>>(parentEntity);
        return parent;
    }

    public async Task<List<string>> GetEmailsAllParents()
    {
        var parentEntity = await _context.Parents
            .Where(p => !string.IsNullOrEmpty(p.Email))
            .AsNoTracking()
            .Select(p => p.Email)
            .ToListAsync();
        
        var emails = _mapper.Map<List<string>>(parentEntity);
        return emails;
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
        string role, string phoneNumber, string email, string address, ICollection<Pupil> pupils)
    {
        var parentEntity = await _context.Parents
            .Include(g => g.Pupils)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (parentEntity == null)
            throw new Exception("Родитель не найден");

        parentEntity.FirstName = firstName;
        parentEntity.LastName = lastName;
        parentEntity.Patronymic = patronymic;
        parentEntity.DateOfBirth = dateOfBirth;
        parentEntity.Role = role;
        parentEntity.PhoneNumber = phoneNumber;
        parentEntity.Email = email;
        parentEntity.Address = address;

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