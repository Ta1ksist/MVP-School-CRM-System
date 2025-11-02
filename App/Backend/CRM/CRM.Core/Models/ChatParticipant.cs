namespace CRM.Core.Models;

public class ChatParticipant
{
    public Guid ChatRoomId { get; set; }
    public ChatRoom ChatRoom { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }
}