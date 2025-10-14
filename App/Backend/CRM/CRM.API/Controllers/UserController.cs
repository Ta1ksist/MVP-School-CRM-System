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
public class UserController : Controller
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("ByUserId/{id}")]
    public async Task<ActionResult<User>> GetUserById(Guid id)
    {
        var user = await _userService.GetUserById(id);
        
        if (user == null) return NotFound();
        
        var response = new UserResponse(
            user.Id,
            user.UserName,
            user.PasswordHash,
            user.Role,
            user.TeacherId,
            user.Teacher,
            user.DirectorateId,
            user.Directorate
        );
        return Ok(response);
    }
    
    [HttpGet("ByUserName/{userName}")]
    public async Task<ActionResult<User>> GetUserByUserName(string userName)
    {
        var user = await _userService.GetUserByUserName(userName);
        
        if (user == null) return NotFound();
        
        var response = new UserResponse(
            user.Id,
            user.UserName,
            user.PasswordHash,
            user.Role,
            user.TeacherId,
            user.Teacher,
            user.DirectorateId,
            user.Directorate
        );
        return Ok(response);
    }

    [HttpGet("ByTeacherId/{teacherId}")]
    public async Task<ActionResult<User>> GetUserTeacherId(Guid teacherId)
    {
        var user = await _userService.GetUserTeacherId(teacherId);
        
        if (user == null) return NotFound();
        
        var response = new UserResponse(
            user.Id,
            user.UserName,
            user.PasswordHash,
            user.Role,
            user.TeacherId,
            user.Teacher,
            user.DirectorateId,
            user.Directorate
        );
        return Ok(response);
    }
    
    [HttpGet("ByDirectorateId/{directorId}")]
    public async Task<ActionResult<User>> GetUserDirectorateId(Guid directorId)
    {
        var user = await _userService.GetUserDirectorateId(directorId);
        
        if (user == null) return NotFound();
        
        var response = new UserResponse(
            user.Id,
            user.UserName,
            user.PasswordHash,
            user.Role,
            user.TeacherId,
            user.Teacher,
            user.DirectorateId,
            user.Directorate
        );
        return Ok(response);
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        var response = users
            .Select(u => new UserResponse(u.Id, u.UserName, u.PasswordHash, u.Role, u.TeacherId, u.Teacher,
                u.DirectorateId, u.Directorate));
        
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> AddUser([FromBody] UserRequest request)
    {
        var user = new User(
            Guid.NewGuid(),
            request.UserName,
            request.PasswordHash,
            request.Role, 
            request.TeacherId,
            request.Teacher,
            request.DirectorateId,
            request.Directorate);
        
        await _userService.AddUser(user);
        return Ok(user);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateUser(Guid id,[FromBody] UserRequest request)
    {
        await _userService.UpdateUser(id, request.UserName, request.PasswordHash, request.Role);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteUser(Guid id)
    {
        await _userService.DeleteUser(id);
        return Ok();
    }
}