using CRM.API.Contracts.Requests;
using CRM.API.Contracts.Responses;
using CRM.Core.Abstractions.Services;
using CRM.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class PupilController : ControllerBase
{
    private readonly IPupilService _pupilService;

    public PupilController(IPupilService pupilService)
    {
        _pupilService = pupilService;
    }

    [HttpGet("ByName/{firstName}-{lastName}")]
    public async Task<ActionResult<Pupil>> GetPupilByName(string firstName, string lastName)
    {
        var pupil = await _pupilService.GetPupilByName(firstName, lastName);
        var response = new PupilResponse(
            pupil.Id,
            pupil.FirstName,
            pupil.LastName,
            pupil.Patronymic,
            pupil.DateOfBirth,
            pupil.GradeId,
            pupil.PhoneNumber,
            pupil.Email,
            pupil.Address
            );
        
        return Ok(response);
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<Pupil>>> GetAllPupils()
    {
        var pupils = await _pupilService.GetAllPupils();
        var response = pupils
            .Select(p => new PupilResponse(p.Id, p.FirstName, p.LastName, p.Patronymic, p.DateOfBirth, p.GradeId,
                p.PhoneNumber, p.Email, p.Address));
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> AddPupil([FromBody] PupilRequest request)
    {
        var pupil = new Pupil(
            Guid.NewGuid(),
            request.FirstName,
            request.LastName,
            request.Patronymic,
            request.DateOfBirth,
            request.GradeId,
            request.PhoneNumber,
            request.Email,
            request.Address
            );
        
        await _pupilService.AddPupil(pupil);
        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdatePupil(Guid id, [FromBody] PupilRequest request)
    {
        await _pupilService.UpdatePupil(id, request.FirstName, request.LastName, request.Patronymic, request.DateOfBirth,
            request.GradeId, request.PhoneNumber, request.Email, request.Address);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeletePupil(Guid id)
    {
        await _pupilService.DeletePupil(id);
        return Ok();
    }
}