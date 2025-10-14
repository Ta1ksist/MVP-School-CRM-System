using CRM.Core.Models;

namespace CRM.Core.Abstractions.Auth;

public interface ITokenService
{
    string GenerateToken(User user);
}