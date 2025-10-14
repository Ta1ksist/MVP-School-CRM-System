using CRM.Core.Abstractions.Auth;

namespace CRM.Application.Services;

public class PasswordHasher : IPasswordHasher
{
    public string PasswordHashing(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public bool Verify(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }
}