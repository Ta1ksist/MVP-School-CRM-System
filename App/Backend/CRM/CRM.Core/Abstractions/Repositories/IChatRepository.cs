using CRM.Core.Models;

namespace CRM.Core.Abstractions.Repositories;

public interface IChatRepository
{
    Task<ChatRoom?> GetRoomById(Guid roomId);
    Task<IEnumerable<ChatRoom>> GetAllRooms();
    Task<IEnumerable<ChatRoom>> GetUserRooms(Guid userId);
    Task AddRoom(ChatRoom room);
    Task<IEnumerable<ChatMessage>> GetMessagesByRoomId(Guid roomId);
    Task AddMessage(ChatMessage message);
}