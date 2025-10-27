namespace CRM.API.Contracts.Requests;

public record EventRequest(
    string Name,
    string Description,
    DateTime Date,
    string PhotoPath);