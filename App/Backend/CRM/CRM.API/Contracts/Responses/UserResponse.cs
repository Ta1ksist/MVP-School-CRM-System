using CRM.Core.Models;

namespace CRM.API.Contracts.Responses;

public record UserResponse(
    Guid Id,
    string UserName,
    string PasswordHash,
    string Role,
    Guid? TeacherId,
    Teacher? Teacher,
    Guid? DirectorateId,
    Directorate? Directorate
    );