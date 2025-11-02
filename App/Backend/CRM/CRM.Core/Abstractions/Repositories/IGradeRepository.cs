using CRM.Core.Models;

namespace CRM.Core.Abstractions.Repositories;

public interface IGradeRepository
{
    Task<Grade> GetGradeByName(string name);
    Task<List<Grade>> GetAllGrades();
    Task<Guid> AddGrade(Grade grade);
    Task AddPupilToGrade(Guid gradeId, Guid pupilId);
    Task<Guid> UpdateGrade(Guid id, string name);
    Task<Guid> DeleteGrade(Guid id);
    Task RemovePupilFromGrade(Guid gradeId, Guid pupilId);
}