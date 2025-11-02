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
public class ClubController : ControllerBase
{
    private readonly IClubService _clubService;

    public ClubController(IClubService clubService)
    {
        _clubService = clubService;
    }

    [HttpGet("ById/{id}")]
    public async Task<ActionResult<Club>> GetClubById(Guid id)
    {
        var club = await _clubService.GetClubById(id);
        if (club == null) return NotFound();
        var clubResponse = new ClubResponse(
            club.Id,
            club.Name,
            club.Description,
            club.MonthlyFee,
            club.IsActive,
            club.Enrollments
            );
        
        return Ok(clubResponse);
    }
    
    [HttpGet("ByName/{name}")]
    public async Task<ActionResult<Club>> GetClubByName(string name)
    {
        var club = await _clubService.GetClubByName(name);
        if (club == null) return NotFound();
        var clubResponse = new ClubResponse(
            club.Id,
            club.Name,
            club.Description,
            club.MonthlyFee,
            club.IsActive,
            club.Enrollments
        );
        
        return Ok(clubResponse);
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<Club>>> GetAllClubs()
    {
        var club = await _clubService.GetAllClubs();
        var response = club
            .Select(c => new ClubResponse(c.Id, c.Name, c.Description, c.MonthlyFee, c.IsActive, c.Enrollments));
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> AddClub([FromBody] ClubRequest request)
    {
        var club = new Club(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.MonthlyFee,
            request.IsActive,
            request.Enrollments
            );

        await _clubService.AddClub(club);
        return Ok(club.Id);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateClub(Guid id, string name, string description, decimal monthlyFee, bool isActive)
    {
        await _clubService.UpdateClub(id, name, description, monthlyFee, isActive);
        return Ok(id);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteClub(Guid id)
    {
        await _clubService.DeleteClub(id);
        return Ok(id);
    }
}