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
        var chatRoomEntity = await _context.ChatRooms
            .Where(cr => cr.Id == roomId)
            .FirstOrDefaultAsync();
        var chatRoom = _mapper.Map<ChatRoom>(chatRoomEntity);
        return chatRoom;
    }

    public async Task<IEnumerable<ChatRoom>> GetAllRooms()
    {
        var chatRoomEntities = await _context.ChatRooms
            .AsNoTracking()
            .ToListAsync();
        var chatRooms = _mapper.Map<IEnumerable<ChatRoom>>(chatRoomEntities);
        return chatRooms;
    }

    public async Task<IEnumerable<ChatRoom>> GetUserRooms(Guid userId)
    {
        string idStr = userId.ToString();
        var chatRoomsEntity = await _context.ChatRooms
            .Where(r => r.Participants.Contains(idStr))
            .ToListAsync();
        var chatRooms = _mapper.Map<IEnumerable<ChatRoom>>(chatRoomsEntity);
        return chatRooms;
    }

    
    public async Task AddRoom(ChatRoom room)
    {
        var chatRoomEntity = _mapper.Map<ChatRoomEntity>(room);
        _context.ChatRooms.Add(chatRoomEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ChatMessage>> GetMessagesByRoomId(Guid roomId)
    {
        var ChatMessageEntity = await _context.ChatMessages
            .Where(m => m.RoomId == roomId)
            .OrderBy(m => m.SentAt)
            .ToListAsync();
        var ChatMessages = _mapper.Map<IEnumerable<ChatMessage>>(ChatMessageEntity);
        return ChatMessages;
    }

    public async Task AddMessage(ChatMessage message)
    {
        var ChatMessageEntity = _mapper.Map<ChatMessageEntity>(message);
        _context.ChatMessages.Add(ChatMessageEntity);
        await _context.SaveChangesAsync();
    }
}