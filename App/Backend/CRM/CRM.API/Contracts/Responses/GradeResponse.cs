using CRM.Core.Models;

namespace CRM.API.Contracts.Responses;

public record GradeResponse(
    Guid Id,
    string Name
    );