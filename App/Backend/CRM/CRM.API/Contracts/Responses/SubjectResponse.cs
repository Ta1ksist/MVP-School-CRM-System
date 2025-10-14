using CRM.Core.Models;

namespace CRM.API.Contracts.Responses;

public record SubjectResponse(
    Guid Id,
    string Name,
    Teacher Teachers
    );