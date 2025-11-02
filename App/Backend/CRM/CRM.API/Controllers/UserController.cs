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
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserResponse>> GetUserById(Guid id)
    {
        var user = await _userService.GetUserById(id);
        if (user == null)
            return NotFound();
        var response = new UserResponse(
            user.Id,
            user.UserName,
            user.Role,
            user.TeacherId,
            user.DirectorateId);
        return Ok(response);
    }

    [HttpGet("ByName/{userName}")]
    public async Task<ActionResult<UserResponse>> GetUserByUserName(string userName)
    {
        var user = await _userService.GetUserByUserName(userName);
        if (user == null) return NotFound();
        var response = new UserResponse(
            user.Id,
            user.UserName,
            user.Role,
            user.TeacherId,
            user.DirectorateId);
        return Ok(response);
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        var response = users
            .Select(u => new UserResponse(u.Id, u.UserName, u.Role, u.TeacherId, u.DirectorateId));
        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Guid>> AddUser([FromBody] UserRequest request)
    {
        var user = new User(
            Guid.NewGuid(),
            request.UserName,
            request.Password,
            request.Role);
        
        await _userService.AddUser(user);
        return Ok(user.Id);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        await _userService.DeleteUser(id);
        return Ok();
    }
}