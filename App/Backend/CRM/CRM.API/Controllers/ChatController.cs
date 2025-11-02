using System.Security.Claims;
using CRM.Core.Abstractions.Services;
using CRM.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

[Authorize(Roles = "Admin, Director, Teacher")]
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
        var userId = GetCurrentUserId();
        var rooms = await _chatService.GetUserRooms(userId);
        return Ok(rooms);
    }

    [HttpGet("room/{roomId:guid}/messages")]
    public async Task<IActionResult> GetMessages(Guid roomId)
    {
        var userId = GetCurrentUserId();

        var rooms = await _chatService.GetUserRooms(userId);
        if (!rooms.Any(r => r.Id == roomId)) return Forbid("У вас нет доступа к этой комнате");

        var messages = await _chatService.GetMessagesByRoomId(roomId);
        return Ok(messages);
    }

    [HttpPost("room")]
    public async Task<IActionResult> CreateRoom([FromBody] ChatRoom room)
    {
        if (room == null) return BadRequest("Комната не может быть пустой");

        var userId = GetCurrentUserId();
        room.AddParticipant(userId);

        await _chatService.AddRoom(room);
        return CreatedAtAction(nameof(GetMessages), new { roomId = room.Id }, room);
    }

    private Guid GetCurrentUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        return claim != null ? Guid.Parse(claim.Value) : Guid.Empty;
    }
}
