using CRM.Core.Models;

namespace CRM.API.Contracts.Requests;

public record ParentRequest(
    string FirstName,
    string LastName,
    string Patronymic,
    DateOnly DateOfBirth,
    string Role,
    string PhoneNumber,
    string Email,
    string Address,
    ICollection<Pupil> Pupils
    );