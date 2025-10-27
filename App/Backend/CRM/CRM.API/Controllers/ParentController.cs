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
public class ParentController : ControllerBase
{
    private readonly IParentService _parentService;
    
    public ParentController(IParentService parentService)
    {
        _parentService = parentService;
    }

    [HttpGet("ByName/{firstName}-{lastName}")]
    public async Task<ActionResult<Parent>> GetParentByName(string firstName, string lastName)
    {
        var parent = await _parentService.GetParentByName(firstName, lastName);
        if (parent == null) return NotFound();
        var response = new ParentResponse(
            parent.Id,
            parent.FirstName,
            parent.LastName,
            parent.Patronymic,
            parent.DateOfBirth,
            parent.Role,
            parent.PhoneNumber,
            parent.Email,
            parent.Address,
            parent.PupilId,
            parent.Pupil
            );
        
        return Ok(response);
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<Parent>>> GetAllParents()
    {
        var parents = await _parentService.GetAllParents();
        var response = parents
            .Select(p => new ParentResponse(p.Id, p.FirstName, p.LastName, p.Patronymic,
                p.DateOfBirth, p.Role, p.PhoneNumber, p.Email, p.Address, p.PupilId, p.Pupil));
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> AddParent([FromBody] ParentRequest request)
    {
        var parent = new Parent(
            Guid.NewGuid(),
            request.FirstName,
            request.LastName,
            request.Patronymic,
            request.DateOfBirth,
            request.Role,
            request.PhoneNumber,
            request.Email,
            request.Address,
            request.PupilId,
            request.Pupil
            );

        await _parentService.AddParent(parent);
        return Ok(parent.Id);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateParent(Guid id, string firstName, string lastName, string patronymic,
        DateOnly dateOfBirth, string role, string phoneNumber, string email, string address, Guid pupilId, Pupil pupil)
    {
        await _parentService.UpdateParent(id, firstName, lastName, patronymic, dateOfBirth, role, 
            phoneNumber, email, address, pupilId, pupil);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteParent(Guid id)
    {
        await _parentService.DeleteParent(id);
        return Ok();
    }
}