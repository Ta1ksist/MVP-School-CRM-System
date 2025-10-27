namespace CRM.API.Contracts.Responses;

public record EventResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime Date,
    string PhotoPath);