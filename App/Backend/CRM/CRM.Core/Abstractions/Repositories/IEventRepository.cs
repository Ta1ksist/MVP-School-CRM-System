using CRM.Core.Models;

namespace CRM.Core.Abstractions.Repositories;

public interface IEventRepository
{
    Task<Event> GetEventByName(string name);
    Task<List<Event>> GetAllEvents();
    Task<Guid> AddEvent(Event eevent);
    Task<Guid> UpdateEvent(Guid id, string name, string description, DateTime date, string photoPath);
    Task<Guid> DeleteEvent(Guid id);
}