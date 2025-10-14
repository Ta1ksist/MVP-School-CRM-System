using CRM.Core.Models;

namespace CRM.API.Contracts.Responses;

public record PupilResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Patronymic,
    DateOnly DateOfBirth,
    Guid GradeId,
    Grade Grade,
    string PhoneNumber,
    string Email,
    string Address,
    ICollection<Parent> Parents
    );