using CRM.Core.Models;

namespace CRM.Core.Abstractions.Repositories;

public interface IPupilRepository
{
    Task<Pupil> GetPupilByName(string firstName, string lastName);
    Task<List<Pupil>> GetAllPupils();
    Task<List<string>> GetEmailsAllPupils();
    Task<Guid> AddPupil(Pupil pupil);
    Task AddParentToPupil(Guid pupilId, Guid parentId);
    Task<Guid> UpdatePupil(Guid id, string firstName, string lastName, string patronymic,
        DateOnly dateOfBirth,
        Guid gradeId, string phoneNumber, string email, string address);
    Task<Guid> DeletePupil(Guid id);
}