using CRM.Core.Abstractions.Services;
using CRM.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CRM.API.Hubs;

[Authorize]
public class ChatHub : Hub
{
    private readonly IChatService _chatService;
    
    public ChatHub(IChatService chatService)
    {
        _chatService = chatService;
    }
    
    public async Task SendMessage(Guid roomId, Guid senderId, string messageText)
    {
        var message = new ChatMessage
        {
            Id = Guid.NewGuid(),
            RoomId = roomId,
            SenderId = senderId,
            Text = messageText,
            SentAt = DateTime.UtcNow
        };

        await _chatService.AddMessage(message);

        await Clients.Group(roomId.ToString())
            .SendAsync("ReceiveMessage", new
            {
                message.Id,
                message.RoomId,
                message.SenderId,
                message.Text,
                message.SentAt
            });
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;

        if (userId != null)
        {
            var roomIds = await _chatService.GetUserRooms(Guid.Parse(userId));

            foreach (var room in roomIds)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, room.Id.ToString());
            }
        }
        await base.OnConnectedAsync();
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}