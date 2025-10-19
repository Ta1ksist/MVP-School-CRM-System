using System.Text.Json.Nodes;

namespace CRM.Core.Models;

public class ChatRoom
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public ChatRoomType Type { get; set; }
    public JsonArray Participants { get; set; }
    
    public ICollection<ChatMessage> Messages { get; set; }
}