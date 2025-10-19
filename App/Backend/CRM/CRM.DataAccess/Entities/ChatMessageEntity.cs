namespace CRM.DataAccess.Entities;

public class ChatMessageEntity
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public ChatRoomEntity Room { get; set; }
    public Guid SenderId { get; set; }
    public string Text { get; set; }
    public DateTime SentAt { get; set; }
}