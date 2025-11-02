using CRM.Core.Models;

namespace CRM.API.Contracts.Requests;

public record TeacherRequest(
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