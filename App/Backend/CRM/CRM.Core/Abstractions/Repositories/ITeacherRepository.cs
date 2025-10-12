using CRM.Core.Models;

namespace CRM.Core.Abstractions.Repositories;

public interface ITeacherRepository
{
    Task<List<Teacher>> GetAllTeachers();
    Task<Guid> AddTeacher(Teacher teacher);
    Task<Guid> UpdateTeacher(Guid id, string firstName, string lastName, string patronymic,
        DateOnly dateOfBirth,
        string photoPath, string phoneNumber, string email, string address, ICollection<Subject> subjects);
    Task<Guid> DeleteTeacher(Guid id);
}