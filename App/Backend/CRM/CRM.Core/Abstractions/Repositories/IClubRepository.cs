using CRM.Core.Models;

namespace CRM.Core.Abstractions.Repositories;

public interface IClubRepository
{
    Task<Club> GetClubById(Guid clubId);
    Task<Club> GetClubByName(string name);
    Task<List<Club>> GetAllClubs();
    Task<Guid> AddClub(Club club);
    Task<Guid> UpdateClub(Guid id, string name, string description, decimal monthlyFee, bool isActive);
    Task<Guid> DeleteClub(Guid id);
}