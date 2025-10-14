using CRM.Core.Models;

namespace CRM.Core.Abstractions.Services;

public interface IGradeService
{
    Task<Grade> GetGradeByName(string name);
    Task<List<Grade>> GetAllGrades();
    Task<Guid> AddGrade(Grade grade);
    Task<Guid> UpdateGrade(Guid id, string name, Pupil pupil);
    Task<Guid> DeleteGrade(Guid id);
}