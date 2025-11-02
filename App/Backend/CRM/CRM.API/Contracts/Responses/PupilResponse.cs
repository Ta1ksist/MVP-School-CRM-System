using CRM.Core.Models;

namespace CRM.API.Contracts.Responses;

public record PupilResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Patronymic,
    DateOnly DateOfBirth,
    Guid GradeId,
    string PhoneNumber,
    string Email,
    string Address
    );