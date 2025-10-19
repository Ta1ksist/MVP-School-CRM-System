using CRM.Core.Abstractions.Repositories;
using CRM.Core.Abstractions.Services;
using CRM.Core.Models;

namespace CRM.Application.Services;

public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository;
    
    public ChatService(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<ChatRoom?> GetRoomById(Guid roomId)
    {
        return await _chatRepository.GetRoomById(roomId);
    }

    public async Task<IEnumerable<ChatRoom>> GetAllRooms()
    {
        return await _chatRepository.GetAllRooms();
    }

    public async Task<IEnumerable<ChatRoom>> GetUserRooms(Guid userId)
    {
        return await _chatRepository.GetUserRooms(userId);
    }

    public async Task AddRoom(ChatRoom room)
    {
        await _chatRepository.AddRoom(room);
    }

    public async Task<IEnumerable<ChatMessage>> GetMessagesByRoomId(Guid roomId)
    {
        return await _chatRepository.GetMessagesByRoomId(roomId);
    }
    
    public async Task AddMessage(ChatMessage message)
    {
        await _chatRepository.AddMessage(message);
    }
}