using CRM.API.Contracts.Requests;
using CRM.API.Contracts.Responses;
using CRM.Core.Abstractions.Services;
using CRM.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

// [Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;
    
    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }
    
    [HttpGet("ById/{id}")]
    public async Task<ActionResult<Event>> GetEventById(Guid id)
    {
        var eevent = await _eventService.GetEventById(id);
        if (eevent == null) return NotFound();
        var eeventResponse = new EventResponse(
            eevent.Id,
            eevent.Name,
            eevent.Description,
            eevent.Date,
            eevent.PhotoPath
            );
        
        return Ok(eeventResponse);
    }

    [HttpGet("ById/{name}")]
    public async Task<ActionResult<Event>> GetEventByName(string name)
    {
        var eevent = await _eventService.GetEventByName(name);
        if (eevent == null) return NotFound();
        var eeventResposne = new EventResponse(
            eevent.Id,
            eevent.Name,
            eevent.Description,
            eevent.Date,
            eevent.PhotoPath
            );
        
        return Ok(eeventResposne);
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<Event>>> GetAllEvents()
    {
        var eevents = await _eventService.GetAllEvents();
        var eeventResposne = eevents
            .Select(e => new EventResponse(e.Id, e.Name, e.Description, e.Date, e.PhotoPath));
        
        return Ok(eeventResposne);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> AddEvent([FromBody] EventRequest request)
    {
        var eevent = new Event(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.Date,
            request.PhotoPath
            );
        
        await _eventService.AddEvent(eevent);
        return Ok(eevent);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateEvent(Guid id, string name, string description, DateTime date,
        string photoPath)
    {
        await _eventService.UpdateEvent(id, name, description, date, photoPath);
        return Ok(id);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteEvent(Guid id)
    {
        await _eventService.DeleteEvent(id);
        return Ok(id);
    }
}