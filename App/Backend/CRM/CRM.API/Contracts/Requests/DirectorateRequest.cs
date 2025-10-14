using CRM.Core.Models;

namespace CRM.API.Contracts.Requests;

public record DirectorateRequest(
    string FirstName,
    string LastName,
    string Patronymic,
    DateOnly DateOfBirth,
    string PhotoPath,
    string Role,
    string PhoneNumber,
    string Email,
    string Address,
    User User,
    Guid UserId
    );