using CRM.Core.Models;

namespace CRM.Core.Abstractions.Services;

public interface ISubjectService
{
    Task<Subject> GetSubjectByName(string name);
    Task<List<Subject>> GetAllSubjects();
    Task<Guid> AddSubject(Subject subject);
    Task<Guid> UpdateSubject(Guid id, string name, Teacher teachers);
    Task<Guid> DeleteSubject(Guid id);
}