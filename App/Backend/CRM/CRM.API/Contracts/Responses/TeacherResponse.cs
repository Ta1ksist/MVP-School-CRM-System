using CRM.Core.Models;

namespace CRM.API.Contracts.Responses;

public record TeacherResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Patronymic,
    DateOnly DateOfBirth,
    string PhotoPath,
    string PhoneNumber,
    string Email,
    string Address,
    User User,
    Guid UserId
    );