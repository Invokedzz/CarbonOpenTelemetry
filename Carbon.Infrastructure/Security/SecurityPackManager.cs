using System.Security.Claims;
using System.Text;
using Carbon.Domain.Contracts.Security;
using Carbon.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Security;

public class SecurityPackManager : ISecurityPackManager
{
    private readonly IConfiguration _configuration;

    public SecurityPackManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
        => GenerateAccessToken(user);

    private string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Name, user.Username),
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString())
        };

        var roles = user.Roles;
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"] ?? string.Empty));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var descriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.Now.AddMinutes(8),
            Issuer = _configuration["Jwt:Issuer"] ?? string.Empty,
            Audience = _configuration["Jwt:Audience"] ?? string.Empty,
            SigningCredentials = credentials,
            Subject = new ClaimsIdentity(claims)
        };

        return new JsonWebTokenHandler().CreateToken(descriptor);
    }
}