using CRM.Core.Abstractions.Repositories;
using CRM.Core.Abstractions.Services;
using CRM.Core.Models;

namespace CRM.Application.Services;

public class ClubService : IClubService
{
    private readonly IClubRepository _clubRepository;

    public ClubService(IClubRepository clubRepository)
    {
        _clubRepository = clubRepository;
    }

    public async Task<Club> GetClubById(Guid clubId)
    {
        return await _clubRepository.GetClubById(clubId);
    }

    public async Task<Club> GetClubByName(string name)
    {
        return await _clubRepository.GetClubByName(name);
    }

    public async Task<List<Club>> GetAllClubs()
    {
        return await _clubRepository.GetAllClubs();
    }

    public async Task<Guid> AddClub(Club club)
    {
        return await _clubRepository.AddClub(club);
    }

    public async Task<Guid> UpdateClub(Guid id, string name, string description, decimal monthlyFee, bool isActive)
    {
        return await _clubRepository.UpdateClub(id, name, description, monthlyFee, isActive);
    }

    public async Task<Guid> DeleteClub(Guid id)
    {
        return await _clubRepository.DeleteClub(id);
    }
}