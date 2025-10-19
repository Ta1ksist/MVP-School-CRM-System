using CRM.Core.Abstractions.Repositories;
using CRM.Core.Abstractions.Services;
using CRM.Core.Models;

namespace CRM.Application.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    
    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Event> GetEventById(Guid id)
    {
        return await _eventRepository.GetEventById(id);
    }
    
    public async Task<Event> GetEventByName(string name)
    {
        return await _eventRepository.GetEventByName(name);
    }
    
    public async Task<List<Event>> GetAllEvents()
    {
        return await _eventRepository.GetAllEvents();
    }

    public async Task<Guid> AddEvent(Event eevent)
    {
        return await _eventRepository.AddEvent(eevent);
    }

    public async Task<Guid> UpdateEvent(Guid id, string name, string description, DateTime date, string photoPath)
    {
        return await _eventRepository.UpdateEvent(id, name, description, date, photoPath);
    }
    
    public async Task<Guid> DeleteEvent(Guid id)
    {
        return await _eventRepository.DeleteEvent(id);
    }
}