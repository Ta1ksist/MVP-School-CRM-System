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
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _teacherService;

    public TeacherController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpGet("ByName/{firstName}-{lastName}")]
    public async Task<ActionResult<Teacher>> GetTeacherByName(string firstName, string lastName)
    {
        var teacher = await _teacherService.GetTeacherByName(firstName, lastName);
        if (teacher == null) return NotFound();
        var response = new TeacherResponse(
            teacher.Id,
            teacher.FirstName,
            teacher.LastName,
            teacher.Patronymic,
            teacher.DateOfBirth,
            teacher.PhotoPath,
            teacher.PhoneNumber,
            teacher.Email,
            teacher.Address,
            teacher.User,
            teacher.UserId
            );
        
        return Ok(response);
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<Teacher>>> GetAllTeachers()
    {
        var teachers = await _teacherService.GetAllTeachers();
        var response = teachers
            .Select(t => new TeacherResponse(t.Id, t.FirstName, t.LastName, t.Patronymic,
                t.DateOfBirth, t.PhotoPath, t.PhoneNumber, t.Email, t.Address, t.User,
            t.UserId));
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> AddTeacher([FromBody] TeacherRequest request)
    {
        var teacher = new Teacher(
            Guid.NewGuid(),
            request.FirstName,
            request.LastName,
            request.Patronymic,
            request.DateOfBirth,
            request.PhotoPath,
            request.PhoneNumber,
            request.Email,
            request.Address,
            request.User,
            request.UserId
            );
        
        await _teacherService.AddTeacher(teacher);
        return Ok(teacher.Id);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateTeacher(Guid id, string firstName, string lastName, string patronymic,
        DateOnly dateOfBirth,
        string photoPath, string phoneNumber, string email, string address, ICollection<Subject> subjects, Guid userId)
    {
        await _teacherService.UpdateTeacher(id, firstName, lastName, patronymic, dateOfBirth, 
            photoPath, phoneNumber, email, address, subjects, userId);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteTeacher(Guid id)
    {
        await _teacherService.DeleteTeacher(id);
        return Ok();
    }
}