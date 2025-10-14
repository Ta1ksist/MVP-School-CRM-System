using CRM.Core.Models;

namespace CRM.Core.Abstractions.Repositories;

public interface IPupilRepository
{
    Task<Pupil> GetPupilByName(string firstName, string lastName);
    Task<List<Pupil>> GetAllPupils();
    Task<Guid> AddPupil(Pupil pupil);
    Task<Guid> UpdatePupil(Guid id, string firstName, string lastName, string patronymic,
        DateOnly dateOfBirth,
        Guid gradeId, Grade grade, string phoneNumber, string email, string address, ICollection<Parent> parents);
    Task<Guid> DeletePupil(Guid id);
}