using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]                          // api/profile
public class ProfileController : ControllerBase
{
    [HttpGet]
    [Authorize]                                      // requiere JWT válido
    public IActionResult Get()
    {
        var user = HttpContext.User;
        var id = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                 ?? user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        var nombre = user.Identity?.Name
                     ?? user.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value;

        var apellido = user.FindFirst("apellido")?.Value;

        return Ok(new { Id = id, Nombre = nombre, Apellido = apellido });
    }
}
