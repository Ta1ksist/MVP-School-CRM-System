using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CRM.Core.Abstractions.Auth;
using CRM.Core.Models;
using CRM.DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CRM.Application.Services;

// public class TokenService : ITokenService
// {
//     private readonly IConfiguration _configuration;
//
//     public TokenService(IConfiguration configuration)
//     {
//         _configuration = configuration;
//     }
//     
//     public string GenerateToken(User user)
//     {
//         var claims = new[]
//         {
//             new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//             new Claim(ClaimTypes.Name, user.UserName),
//             new Claim("role", user.Role)
//         };
//     
//         var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
//         var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//     
//         var token = new JwtSecurityToken(
//             issuer: _configuration["Jwt:Issuer"],
//             audience: _configuration["Jwt:Audience"],
//             claims: claims,
//             expires: DateTime.UtcNow.AddHours(1),
//             signingCredentials: creds);
//     
//         return new JwtSecurityTokenHandler().WriteToken(token);
//     }
// }
public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var roleValue = user.Role ?? string.Empty;

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty)
        };

        if (!string.IsNullOrEmpty(roleValue))
        {
            claims.Add(new Claim(ClaimTypes.Role, roleValue));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}