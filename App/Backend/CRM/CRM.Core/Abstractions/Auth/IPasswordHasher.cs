namespace CRM.Core.Abstractions.Auth;

public interface IPasswordHasher
{
    string PasswordHashing(string password);
    bool Verify(string password, string hashedPassword);
}