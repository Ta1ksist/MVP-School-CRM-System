using System.Text.Json.Nodes;
using CRM.Core.Models;

namespace CRM.DataAccess.Entities;

public class ChatRoomEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public ChatRoomType Type { get; set; }
    public JsonArray Participants { get; set; }
    
    public ICollection<ChatMessageEntity> Messages { get; set; }
}