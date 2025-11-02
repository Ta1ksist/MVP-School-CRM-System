using CRM.Core.Models;

namespace CRM.API.Contracts.Responses;

public record UserResponse(
    Guid Id,
    string UserName,
    string Role,
    Guid? TeacherId,
    Guid? DirectorateId
    );