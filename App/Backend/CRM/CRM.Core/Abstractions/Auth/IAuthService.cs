using CRM.Core.DTOs;

namespace CRM.Core.Abstractions.Auth;

public interface IAuthService
{
    Task<string> Login(LoginDTO dto);
    Task Register(RegisterDTO dto);
}