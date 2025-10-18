using AutoMapper;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.Models;
using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DataAccess.Repositories;

public class EventRepository : IEventRepository
{
    private readonly CRMContext _context;
    private readonly IMapper _mapper;
    
    public EventRepository(CRMContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Event> GetEventByName(string name)
    {
        var eventEntity = await _context.Events
            .Where(e => e.Name == name)
            .FirstOrDefaultAsync();
        if (eventEntity == null) throw new InvalidOperationException("Событие/мероприятие не нашлось");
        
        var eevent = _mapper.Map<Event>(eventEntity);
        
        return eevent;
    }
    
    public async Task<List<Event>> GetAllEvents()
    {
        var eventEntity = await _context.Events
            .AsNoTracking()
            .ToListAsync();
        var eevent = _mapper.Map<List<Event>>(eventEntity);
        return eevent;
    }

    public async Task<Guid> AddEvent(Event eevent)
    {
        var eventEntity = _mapper.Map<EventEntity>(eevent);

        await _context.Events.AddAsync(eventEntity);
        await _context.SaveChangesAsync();
        return eventEntity.Id;
    }

    public async Task<Guid> UpdateEvent(Guid id, string name, string description, DateTime date, string photoPath)
    {
        var eventEntity = await _context.Events
            .Where(e => e.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(e => e.Name, name)
                .SetProperty(e => e.Description, description)
                .SetProperty(e => e.Date, date)
                .SetProperty(e => e.PhotoPath, photoPath));

        return id;
    }

    public async Task<Guid> DeleteEvent(Guid id)
    {
        var eventEntity = await _context.Events
            .Where(e => e.Id == id)
            .ExecuteDeleteAsync();
        
        return id;
    }
}