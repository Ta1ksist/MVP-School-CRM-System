namespace CRM.DataAccess.Entities;

public class ChatParticipantEntity
{
    public Guid ChatRoomId { get; set; }
    public ChatRoomEntity ChatRoom { get; set; }

    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
}