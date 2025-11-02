using System.Text.Json.Nodes;
using CRM.Core.Models;

namespace CRM.DataAccess.Entities;

public class ChatRoomEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public ChatRoomType Type { get; set; }

    public ICollection<ChatParticipantEntity> Participants { get; set; } = new List<ChatParticipantEntity>();
    public ICollection<ChatMessageEntity> Messages { get; set; }
}