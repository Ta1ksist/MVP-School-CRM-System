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
public class GradeController : ControllerBase
{
    private readonly IGradeService _gradeService;
    
    public GradeController(IGradeService gradeService)
    {
        _gradeService = gradeService;
    }

    [HttpGet("ByName/{name}")]
    public async Task<ActionResult<Grade>> GetGradeByName(string name)
    {
        var grade = await _gradeService.GetGradeByName(name);
        var response = new GradeResponse(
            grade.Id,
            grade.Name
            );
        
        return Ok(response);
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<Grade>>> GetAllGrades()
    {
        var grades = await _gradeService.GetAllGrades();
        var response = grades
            .Select(g => new GradeResponse(g.Id, g.Name));
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> AddGrade([FromBody] GradeRequest request)
    {
        var grade = new Grade(
            Guid.NewGuid(),
            request.Name
            );
        
        await _gradeService.AddGrade(grade);
        return Ok(grade.Id);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateGrade(Guid id, [FromBody] GradeRequest request)
    {
        await _gradeService.UpdateGrade(id, request.Name);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteGrade(Guid id)
    {
        await _gradeService.DeleteGrade(id);
        return Ok();
    }
}