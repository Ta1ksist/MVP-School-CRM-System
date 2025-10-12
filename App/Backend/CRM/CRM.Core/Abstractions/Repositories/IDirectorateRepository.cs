using CRM.Core.Models;

namespace CRM.Core.Abstractions.Repositories;

public interface IDirectorateRepository
{
    Task<List<Directorate>> GetAllDirectorates();
    Task<Guid> AddDirectorate(Directorate directorate);
    Task<Guid> UpdateDirectorate(Guid id, string firstName, string lastName, string patronymic,
        DateOnly dateOfBirth, string photoPath, string role, string phoneNumber, string email, string address);

    Task<Guid> DeleteDirectorate(Guid id);
}