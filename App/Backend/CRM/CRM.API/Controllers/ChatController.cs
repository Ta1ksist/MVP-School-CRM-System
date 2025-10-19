using System.Security.Claims;
using CRM.Core.Abstractions.Services;
using CRM.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpGet("rooms")]
    public async Task<IActionResult> GetRooms()
    {
        var userId = GetCurrentUserId(); // метод, достающий userId из токена
        var rooms = await _chatService.GetUserRooms(userId);
        return Ok(rooms);
    }

    [HttpGet("room/{roomId}/messages")]
    public async Task<IActionResult> GetMessages(Guid roomId)
    {
        var messages = await _chatService.GetMessagesByRoomId(roomId);
        return Ok(messages);
    }

    [HttpPost("room")]
    public async Task<IActionResult> CreateRoom([FromBody] ChatRoom room)
    {
        await _chatService.AddRoom(room);
        return Ok();
    }

    private Guid GetCurrentUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        return claim != null ? Guid.Parse(claim.Value) : Guid.Empty;
    }
}
