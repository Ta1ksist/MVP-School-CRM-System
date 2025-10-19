using AutoMapper;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.Models;
using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DataAccess.Repositories;

public class DirectorateRepository : IDirectorateRepository
{
    private readonly CRMContext _context;
    private readonly IMapper _mapper;
    
    public DirectorateRepository(CRMContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Directorate> GetDirectorateById(Guid id)
    {
        var directorateEntity = await _context.Directorates
            .Include(d => d.User)
            .Where(d => d.Id == id).FirstOrDefaultAsync();
        var directorate = _mapper.Map<Directorate>(directorateEntity);
        
        return directorate;
    }
    
    public async Task<Directorate> GetDirectorateByName(string firstName, string lastName)
    {
        var directorateEntity = await _context.Directorates
            .Include(t => t.User)
            .Where(d => d.FirstName == firstName && d.LastName == lastName)
            .FirstOrDefaultAsync();
        var directorate = _mapper.Map<Directorate>(directorateEntity);
        
        return directorate;
    }
    
    public async Task<List<Directorate>> GetAllDirectorates()
    {
        var directoratesEntity = await _context.Directorates
            .Include(t => t.User)
            .AsNoTracking()
            .ToListAsync();
        var directorates = _mapper.Map<List<Directorate>>(directoratesEntity);
        
        return directorates;
    }

    public async Task<List<string>> GetEmailsAllDirectorates()
    {
        var directoratesEntity = await _context.Directorates
            .Include(t => t.User)
            .AsNoTracking()
            .Where(d => !string.IsNullOrEmpty(d.Email))
            .Select(d => d.Email)
            .ToListAsync();
        var directorates = _mapper.Map<List<string>>(directoratesEntity);
        return directorates;
    }
    
    public async Task<Guid> AddDirectorate(Directorate directorate)
    {
        var directorateEntity = _mapper.Map<DirectorateEntity>(directorate);

        await _context.Directorates.AddAsync(directorateEntity);
        await _context.SaveChangesAsync();
        
        return directorateEntity.Id;
    }

    public async Task<Guid> UpdateDirectorate(Guid id, string firstName, string lastName, string patronymic,
        DateOnly dateOfBirth, string photoPath, string role, string phoneNumber, string email, string address, Guid userId)
    {
        var directorateEntity = await _context.Directorates.Where(d => d.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(d => d.FirstName, firstName)
                .SetProperty(d => d.LastName, lastName)
                .SetProperty(d => d.Patronymic, patronymic)
                .SetProperty(d => d.DateOfBirth, dateOfBirth)
                .SetProperty(d => d.PhotoPath, photoPath)
                .SetProperty(d => d.Role, role)
                .SetProperty(d => d.PhoneNumber, phoneNumber)
                .SetProperty(d => d.Email, email)
                .SetProperty(d => d.Address, address)
                .SetProperty(d => d.UserId, userId));

        return id;
    }

    public async Task<Guid> DeleteDirectorate(Guid id)
    {
        var directorateEntity = await _context.Directorates.Where(d => d.Id == id)
            .ExecuteDeleteAsync();
        
        return id;
    }
}