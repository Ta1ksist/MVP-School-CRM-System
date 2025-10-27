namespace CRM.API.Contracts.Requests;

public record NewsRequest(
    string Title,
    string Description,
    DateTime Date,
    string PhotoPath);