using CRM.Core.Abstractions.Auth;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.Abstractions.Services;
using CRM.Core.DTOs;
using CRM.Core.Models;

namespace CRM.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    
    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }
    
    public async Task<User> GetUserById(Guid id)
    {
        return await _userRepository.GetUserById(id);
    }
    
    public async Task<User> GetUserByUserName(string userName)
    {
        return await _userRepository.GetUserByUsername(userName);
    }
    
    public async Task<User> GetUserTeacherId(Guid teacherId)
    {
        return await _userRepository.GetUserTeacherId(teacherId);
    }

    public async Task<User> GetUserDirectorateId(Guid directorId)
    {
        return await _userRepository.GetUserDirectorateId(directorId);
    }
    
    public async Task<List<User>> GetAllUsers()
    {
        return await _userRepository.GetAllUsers();
    }

    public async Task<Guid> AddUser(User user)
    {
        return await _userRepository.AddUser(user);
    }
    
    public async Task<Guid> UpdateUser(Guid id, string userName, string passwordHash, string role,
        Guid? teacherId, Guid? directorateId)
    {
        return await _userRepository.UpdateUser(id, userName, passwordHash, role, teacherId, directorateId);
    }
    
    public async Task<Guid> DeleteUser(Guid id)
    {
        return await _userRepository.DeleteUser(id);
    }
}