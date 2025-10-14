using CRM.Core.Models;

namespace CRM.API.Contracts.Requests;

public record UserRequest(
    string UserName,
    string PasswordHash,
    string Role,
    Guid? TeacherId,
    Teacher? Teacher,
    Guid? DirectorateId,
    Directorate? Directorate
    );