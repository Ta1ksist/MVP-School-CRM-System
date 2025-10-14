using CRM.Core.Models;

namespace CRM.Core.Abstractions.Repositories;

public interface IUserRepository
{
    Task<User> GetUserById(Guid id);
    Task<User> GetUserByUsername(string userName);
    Task<User> GetUserTeacherId(Guid teacherId);
    Task<User> GetUserDirectorateId(Guid directorateId);
    Task<List<User>> GetAllUsers();
    Task<Guid> AddUser(User user);
    Task<Guid> UpdateUser(Guid id, string userName, string passwordHash, string role,
        Guid? teacherId, Guid? directorateId);
    Task<Guid> DeleteUser(Guid id);
}