using CRM.Core.Models;

namespace CRM.API.Contracts.Responses;

public record ParentResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Patronymic,
    DateOnly DateOfBirth,
    string Role,
    string PhoneNumber,
    string Email,
    string Address,
    Guid PupilId,
    Pupil Pupil
    );