using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Security;

public class JwtOptions
{
    public string Issuer { get; set; } = "";
    public string Audience { get; set; } = "";
    public string Key { get; set; } = "";      // 32+ chars
    public int ExpMinutes { get; set; } = 60;
}

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtOptions _opt;
    public JwtTokenService(IOptions<JwtOptions> opt) => _opt = opt.Value;

    public string Generate(Usuario user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.IdUsuario.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Nombre),
            new Claim("apellido", user.Apellido ?? string.Empty)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _opt.Issuer,
            audience: _opt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_opt.ExpMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
