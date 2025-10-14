using CRM.Core.Abstractions.Auth;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.DTOs;
using CRM.Core.Models;

namespace CRM.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IDirectorateRepository _directorateRepository;
    private readonly ITokenService _tokenService; 
    
    public AuthService(IUserRepository userRepository, ITeacherRepository teacherRepository, 
        IDirectorateRepository directorateRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _teacherRepository = teacherRepository;
        _directorateRepository = directorateRepository;
        _tokenService = tokenService;
    }
    
    public async Task<string> Login(LoginDTO dto)
    {
        var user = await _userRepository.GetUserByUsername(dto.UserName);
        if (user == null) throw new UnauthorizedAccessException("Неверное имя пользователя или пароль");

        var isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
        if (!isPasswordValid) throw new UnauthorizedAccessException("Неверное имя пользователя или пароль");

        var token = _tokenService.GenerateToken(user);
        
        return token;
    }

    public async Task Register(RegisterDTO dto)
    {
        var existingUser = await _userRepository.GetUserByUsername(dto.UserName);
        if (existingUser != null) throw new InvalidOperationException("Пользователь с таким именем уже существует");

        Teacher teacher = null;
        Directorate director = null;

        if (dto.Role == "Teacher")
        {
            if (dto.TeacherId == null) throw new ArgumentException("TeacherId обязателен для роли Teacher");

            teacher = await _teacherRepository.GetTeacherById(dto.TeacherId.Value);
            if (teacher == null) throw new ArgumentException("Учитель не найден");

            var userWithTeacher = await _userRepository.GetUserTeacherId(dto.TeacherId.Value);
            if (userWithTeacher != null) throw new InvalidOperationException("К этому учителю уже привязан пользователь");
        }
        else if (dto.Role == "Directorate")
        {
            if (dto.DirectorateId == null) throw new ArgumentException("DirectorateId обязателен для роли Directorate");

            director = await _directorateRepository.GetDirectorateById(dto.DirectorateId.Value);
            if (director == null) throw new ArgumentException("Член дирекции не найден");

            var userWithDirector = await _userRepository.GetUserDirectorateId(dto.DirectorateId.Value);
            if (userWithDirector != null) throw new InvalidOperationException("К этому члену дирекции уже привязан пользователь");
        }
        else
        {
            throw new ArgumentException("Неизвестная роль");
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var userId = Guid.NewGuid();

        var (user, error) = User.Create(
            userId,
            dto.UserName,
            passwordHash,
            dto.Role,
            dto.TeacherId,
            teacher,
            dto.DirectorateId,
            director
        );

        if (!string.IsNullOrWhiteSpace(error)) throw new ArgumentException(error);

        await _userRepository.AddUser(user);
    }
}