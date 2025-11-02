using CRM.Core.Models;

namespace CRM.Core.Abstractions.Services;

public interface ITeacherService
{
    Task<Teacher> GetTeacherByName(string firstName, string lastName);
    Task<List<Teacher>> GetAllTeachers();
    Task<List<string>> GetEmailsAllTeachers();
    Task<Guid> AddTeacher(Teacher teacher);
    Task AddSubjectToTeacher(Guid teacherId, Guid subjectId);
    Task<Guid> UpdateTeacher(Guid id, string firstName, string lastName, string patronymic,
        DateOnly dateOfBirth,
        string photoPath, string phoneNumber, string email, string address, ICollection<Subject> subjects, Guid userId);
    Task<Guid> DeleteTeacher(Guid id);
}