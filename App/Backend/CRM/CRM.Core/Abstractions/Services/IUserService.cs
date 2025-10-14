using CRM.Core.Models;

namespace CRM.Core.Abstractions.Services;

public interface IUserService
{
    Task<User> GetUserById(Guid id);
    Task<User> GetUserByUserName(string userName);
    Task<User> GetUserTeacherId(Guid teacherId);
    Task<User> GetUserDirectorateId(Guid directorId);
    Task<List<User>> GetAllUsers();
    Task<Guid> AddUser(User user);
    Task<Guid> UpdateUser(Guid id, string userName, string passwordHash, string role);
    Task<Guid> DeleteUser(Guid id);
}