using CRM.Core.Abstractions.Auth;
using CRM.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
    {
        await _authService.Register(dto);
        return Ok(new { message = "Регистрация успешно выполнена" });
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        var token = await _authService.Login(dto);
        return Ok(new { token });
    }
    
    [HttpGet("admin-test")]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult AdminTest()
    {
        return Ok("У тебя есть доступ Admin");
    }
}