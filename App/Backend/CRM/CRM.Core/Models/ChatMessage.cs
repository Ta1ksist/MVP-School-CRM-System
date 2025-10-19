namespace CRM.Core.Models;

public class ChatMessage
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public ChatRoom Room { get; set; }
    public Guid SenderId { get; set; }
    public string Text { get; set; }
    public DateTime SentAt { get; set; }
}