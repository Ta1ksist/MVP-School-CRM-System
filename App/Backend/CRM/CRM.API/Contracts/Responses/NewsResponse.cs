namespace CRM.API.Contracts.Responses;

public record NewsResponse(
    Guid Id,
    string Title,
    string Description,
    DateTime Date,
    string PhotoPath);