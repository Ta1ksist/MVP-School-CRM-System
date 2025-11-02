using System.Text.Json.Nodes;

namespace CRM.Core.Models;

public class ChatRoom
{
    private readonly List<Guid> _participants = new();
    public IReadOnlyCollection<Guid> Participants => _participants.AsReadOnly();

    public Guid Id { get; }
    public string Title { get; private set; }
    public ChatRoomType Type { get; private set; }
    public IReadOnlyCollection<ChatMessage> Messages => _messages.AsReadOnly();
    private readonly List<ChatMessage> _messages = new();

    public ChatRoom(Guid id, string title, ChatRoomType type)
    {
        Id = id;
        Title = title;
        Type = type;
    }

    public void AddParticipant(Guid userId)
    {
        if (!_participants.Contains(userId))
            _participants.Add(userId);
    }

    public void RemoveParticipant(Guid userId)
    {
        _participants.Remove(userId);
    }
}