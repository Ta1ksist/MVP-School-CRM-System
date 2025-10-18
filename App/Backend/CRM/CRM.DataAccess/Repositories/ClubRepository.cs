using AutoMapper;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.Models;
using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DataAccess.Repositories;

public class СlubRepository : IClubRepository
{
    private readonly CRMContext _context;
    private readonly IMapper _mapper;

    public СlubRepository(CRMContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Club> GetClubById(Guid clubId)
    {
        var clubEntity = await _context.Clubs
            .Include(c => c.Enrollments)
            .Where(c => c.Id == clubId)
            .FirstOrDefaultAsync();
        var club = _mapper.Map<ClubEntity, Club>(clubEntity);
        return club;
    }
    
    public async Task<Club> GetClubByName(string name)
    {
        var clubentity = await _context.Clubs
            .Include(c => c.Enrollments)
            .Where(c => c.Name == name)
            .FirstOrDefaultAsync();
        var club = _mapper.Map<ClubEntity, Club>(clubentity);
        
        return club;
    }
    
    public async Task<List<Club>> GetAllClubs()
    {
        var clubEntity = await _context.Clubs
            .Include(c => c.Enrollments)
            .AsNoTracking()
            .ToListAsync();
        var clubs = _mapper.Map<List<Club>>(clubEntity);

        return clubs;
    }

    public async Task<Guid> AddClub(Club club)
    {
        var clubEntity = _mapper.Map<ClubEntity>(club);
        
        await _context.AddAsync(clubEntity);
        await _context.SaveChangesAsync();
        
        return clubEntity.Id;
    }

    public async Task<Guid> UpdateClub(Guid id, string name, string description, decimal monthlyFee,  bool isActive)
    {
        var clubEntity = await _context.Clubs
            .Include(c => c.Enrollments)
            .Where(c => c.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.Name, name)
                .SetProperty(c => c.Description, description)
                .SetProperty(c => c.MonthlyFee, monthlyFee)
                .SetProperty(c => c.IsActive, isActive));
        
        if (clubEntity == 0) throw new Exception("Кружок/секция не найден");
        
        var club = _mapper.Map<Club>(clubEntity);
        await _context.SaveChangesAsync();

        return club.Id;
    }


    public async Task<Guid> DeleteClub(Guid id)
    {
        var clubEntity = await _context.Clubs
            .Include(c => c.Enrollments)
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        
        return id;
    }
}