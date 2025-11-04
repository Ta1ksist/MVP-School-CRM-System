using AutoMapper;
using CRM.Core.Abstractions.Auth;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.DTOs;
using CRM.Core.Models;
using CRM.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

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

         if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash)) 
             throw new UnauthorizedAccessException("Неверное имя пользователя или пароль");

         return _tokenService.GenerateToken(user);
     }

     public async Task Register(RegisterDTO dto)
     {
         var existing = await _userRepository.GetUserByUsername(dto.UserName);
         if (existing != null)
             throw new InvalidOperationException("Пользователь с таким именем уже существует");

         Guid? teacherId = null;
         Guid? directorateId = null;

         if (dto.Role == "Teacher")
         {
             if (dto.TeacherId == null)
                 throw new ArgumentException("TeacherId обязателен для роли Teacher");

             teacherId = dto.TeacherId;
         }
         else if (dto.Role == "Directorate")
         {
             if (dto.DirectorateId == null)
                 throw new ArgumentException("DirectorateId обязателен для роли Directorate");

             directorateId = dto.DirectorateId;
         }
         else
         {
             throw new ArgumentException("Недопустимая роль");
         }

         var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
         var id = Guid.NewGuid();

         var (user, error) = User.Create(
             id,
             dto.UserName,
             passwordHash,
             dto.Role,
             teacherId,
             null,
             directorateId,
             null
         );

        if (!string.IsNullOrWhiteSpace(error))
             throw new ArgumentException(error);

         await _userRepository.AddUser(user);
     }
}
// public class AuthService : IAuthService
// {
//     private readonly UserManager<UserEntity> _userManager;
//     private readonly ITokenService _tokenService;
//
//     public AuthService(UserManager<UserEntity> userManager, ITokenService tokenService)
//     {
//         _userManager = userManager;
//         _tokenService = tokenService;
//     }
//
//     public async Task<string> Login(LoginDTO dto)
//     {
//         var userEntity = await _userManager.FindByNameAsync(dto.UserName);
//         if (userEntity == null)
//             throw new UnauthorizedAccessException("Неверное имя пользователя или пароль");
//
//         if (!await _userManager.CheckPasswordAsync(userEntity, dto.Password))
//             throw new UnauthorizedAccessException("Неверное имя пользователя или пароль");
//
//         var roles = await _userManager.GetRolesAsync(userEntity);
//
//         var domainUser = new User(
//             userEntity.Id,
//             userEntity.UserName!,
//             userEntity.PasswordHash!,
//             roles.FirstOrDefault() ?? "User"
//         );
//
//         return _tokenService.GenerateToken(domainUser);
//     }
//
//     public async Task Register(RegisterDTO dto)
//     {
//         var userEntity = new UserEntity
//         {
//             UserName = dto.UserName
//         };
//
//         var result = await _userManager.CreateAsync(userEntity, dto.Password);
//         if (!result.Succeeded)
//             throw new ArgumentException(string.Join(", ", result.Errors.Select(e => e.Description)));
//
//         await _userManager.AddToRoleAsync(userEntity, dto.Role);
//     }
// }