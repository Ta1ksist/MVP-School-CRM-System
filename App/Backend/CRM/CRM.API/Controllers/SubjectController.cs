using CRM.API.Contracts.Requests;
using CRM.API.Contracts.Responses;
using CRM.Core.Abstractions.Services;
using CRM.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectController : ControllerBase
{
    private readonly ISubjectService _subjectService;
    
    public SubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    [HttpGet("ByName/{name}")]
    public async Task<ActionResult<Subject>> GetSubjectByName(string name)
    {
        var subject = await _subjectService.GetSubjectByName(name);
        
        if (subject == null) return NotFound();
        
        var response = new SubjectResponse(
            subject.Id,
            subject.Name,
            subject.Teachers
            );
        
        return Ok(response);
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<List<Subject>>> GetAllSubjects()
    {
        var subjects = await _subjectService.GetAllSubjects();
        var response = subjects
            .Select(s => new SubjectResponse(s.Id, s.Name, s.Teachers));
        
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> AddSubject([FromBody] SubjectRequest request)
    {
        var subject = new Subject(
            Guid.NewGuid(),
            request.Name,
            request.Teachers
        );
        
        await _subjectService.AddSubject(subject);
        return Ok(subject.Id);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateSubject(Guid id, [FromBody] SubjectRequest request)
    {
        await _subjectService.UpdateSubject(id, request.Name, request.Teachers);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteSubject(Guid id)
    {
        await _subjectService.DeleteSubject(id);
        return Ok();
    }
}