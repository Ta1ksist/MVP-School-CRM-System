using AutoMapper;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.Models;
using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DataAccess.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly CRMContext _context;
    private readonly IMapper _mapper;
    
    public ChatRepository(CRMContext context,  IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ChatRoom?> GetRoomById(Guid roomId)
    {
        var entity = await _context.ChatRooms
            .Include(r => r.Participants)
            .Include(r => r.Messages)
            .FirstOrDefaultAsync(r => r.Id == roomId);

        return _mapper.Map<ChatRoom>(entity);
    }

    public async Task<IEnumerable<ChatRoom>> GetAllRooms()
    {
        var entities = await _context.ChatRooms
            .Include(r => r.Participants)
            .Include(r => r.Messages)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<ChatRoom>>(entities);
    }

    public async Task<IEnumerable<ChatRoom>> GetUserRooms(Guid userId)
    {
        var entities = await _context.ChatRooms
            .Include(r => r.Participants)
            .Include(r => r.Messages)
            .Where(r => r.Participants.Any(p => p.UserId == userId))
            .ToListAsync();

        return _mapper.Map<IEnumerable<ChatRoom>>(entities);
    }
    
    public async Task AddRoom(ChatRoom room)
    {
        var entity = _mapper.Map<ChatRoomEntity>(room);
        _context.ChatRooms.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ChatMessage>> GetMessagesByRoomId(Guid roomId)
    {
        var entities = await _context.ChatMessages
            .Where(m => m.RoomId == roomId)
            .OrderBy(m => m.SentAt)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<ChatMessage>>(entities);
    }

    public async Task AddMessage(ChatMessage message)
    {
        var entity = _mapper.Map<ChatMessageEntity>(message);
        _context.ChatMessages.Add(entity);
        await _context.SaveChangesAsync();
    }
}