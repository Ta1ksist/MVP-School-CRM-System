using CRM.Core.Models;

namespace CRM.Core.Abstractions.Repositories;

public interface ISubjectRepository
{
    Task<Subject> GetSubjectByName(string name);
    Task<List<Subject>> GetAllSubjects();
    Task<Guid> AddSubject(Subject subject);
    Task<Guid> UpdateSubject(Guid id, string name, Teacher teachers);
    Task<Guid> DeleteSubject(Guid id);
}