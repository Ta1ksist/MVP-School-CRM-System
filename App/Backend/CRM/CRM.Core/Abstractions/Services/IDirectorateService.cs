using CRM.Core.Models;

namespace CRM.Core.Abstractions.Services;

public interface IDirectorateService
{
    Task<Directorate> GetDirectorateByName(string firstName, string lastName);
    Task<List<Directorate>> GetAllDirectorates();
    Task<Guid> AddDirectorate(Directorate directorate);
    Task<Guid> UpdateDirectorate(Guid id, string firstName, string lastName, string patronymic,
        DateOnly dateOfBirth, string photoPath, string role, string phoneNumber, string email, string address, Guid userId);
    Task<Guid> DeleteDirectorate(Guid id);
}