using CRM.Core.Models;

namespace CRM.Core.Abstractions.Services;

public interface IParentService
{
    Task<Parent> GetParentByName(string firstName, string lastName);
    Task<List<Parent>> GetAllParents();
    Task<Guid> AddParent(Parent parent);
    Task<Guid> UpdateParent(Guid id, string firstName, string lastName, string patronymic,
        DateOnly dateOfBirth,
        string role, string phoneNumber, string email, string address, Guid pupilId, Pupil pupil);
    Task<Guid> DeleteParent(Guid id);
}