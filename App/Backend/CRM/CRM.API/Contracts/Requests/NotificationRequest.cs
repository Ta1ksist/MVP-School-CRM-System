using CRM.Core.Models;

namespace CRM.API.Contracts.Requests;

public record NotificationRequest(
    IEnumerable<string> Emails,
    Event Event,
    News News
    );