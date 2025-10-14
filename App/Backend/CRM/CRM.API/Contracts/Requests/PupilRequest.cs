using CRM.Core.Models;

namespace CRM.API.Contracts.Requests;

public record PupilRequest(
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