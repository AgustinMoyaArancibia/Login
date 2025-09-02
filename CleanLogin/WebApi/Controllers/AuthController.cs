using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILoginService _login;
    private readonly IRegisterService _register;

    public AuthController(ILoginService login, IRegisterService register)
    {
        _login = login; _register = register;
    }

    /// <summary>Registra un usuario básico (Nombre único) y guarda el hash.</summary>
    /// <response code="201">Creado</response>
    /// <response code="409">Nombre duplicado</response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(CreateUsuarioResponse), 201)]
    [ProducesResponseType(409)]
    public async Task<IActionResult> Register([FromBody] CreateUsuarioRequest req, CancellationToken ct)
    {
        try
        {
            var res = await _register.RegisterAsync(req, ct);
            // Devuelvo 201 + ubicación del recurso hipotética
            return CreatedAtAction(nameof(Register), new { id = res.IdUsuario }, res);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("ya existe"))
        {
            return Conflict(new { error = ex.Message });
        }
    }

    /// <summary>Login y obtención de token JWT.</summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req, CancellationToken ct)
    {
        try { return Ok(await _login.LoginAsync(req, ct)); }
        catch (UnauthorizedAccessException) { return Unauthorized(); }
    }
}
