using CRM.Core.Models;

namespace CRM.Core.Abstractions.Repositories;

public interface IGradeRepository
{
    Task<List<Grade>> GetAllGrades();
    Task<Guid> AddGrade(Grade grade);
    Task<Guid> UpdateGrade(Guid id, string name, Pupil pupil);
    Task<Guid> DeleteGrade(Guid id);
}