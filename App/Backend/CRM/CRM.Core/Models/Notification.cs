namespace CRM.Core.Models;

public class Notification
{
    public Guid Id { get; set; }
    public Guid receivingId { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsRead { get; set; }
}