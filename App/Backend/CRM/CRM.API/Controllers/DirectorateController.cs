using CRM.API.Contracts.Requests;
using CRM.API.Contracts.Responses;
using CRM.Core.Abstractions.Services;
using CRM.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DirectorateController : ControllerBase
{
    private readonly IDirectorateService _directorateService;

    public DirectorateController(IDirectorateService directorateService)
    {
        _directorateService = directorateService;
    }
    
    [HttpGet("ByName/{firstName}-{lastName}")]
    public async Task<ActionResult<Directorate>> GetDirectorateByName(string firstName, string lastName)
    {
        var directorate = await _directorateService.GetDirectorateByName(firstName, lastName);
        if (directorate == null) return NotFound();
        var response = new DirectorateResponse(
            directorate.Id,
            directorate.FirstName,
            directorate.LastName,
            directorate.Patronymic,
            directorate.DateOfBirth,
            directorate.PhotoPath,
            directorate.Role,
            directorate.PhoneNumber,
            directorate.Email,
            directorate.Address,
            directorate.User,
            directorate.UserId
        );
        
        return Ok(response);
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<List<Directorate>>> GetAllDirectorates()
    {
        var directorate = await _directorateService.GetAllDirectorates();
        var response = directorate
            .Select(d => new DirectorateResponse(d.Id, d.FirstName, d.LastName, d.Patronymic, d.DateOfBirth,
                d.PhotoPath, d.Role, d.PhoneNumber, d.Email, d.Address, d.User, d.UserId));
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> AddDirectorate([FromBody] DirectorateRequest request)
    {
        var director = new Directorate(
            Guid.NewGuid(),
            request.FirstName,
            request.LastName,
            request.Patronymic,
            request.DateOfBirth,
            request.PhotoPath,
            request.Role,
            request.PhoneNumber,
            request.Email,
            request.Address,
            request.User,
            request.UserId
        );
        
        await _directorateService.AddDirectorate(director);
        return Ok(director.Id);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateDirectorate(Guid id, string firstName, string lastName, string patronymic,
        DateOnly dateOfBirth, string photoPath, string role, string phoneNumber, string email, string address, Guid userId)
    {
        await _directorateService.UpdateDirectorate(id, firstName, lastName, patronymic, dateOfBirth, photoPath,
            role, phoneNumber, email, address, userId);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteDirectorate(Guid id)
    {
        await _directorateService.DeleteDirectorate(id);
        return Ok();
    }
}