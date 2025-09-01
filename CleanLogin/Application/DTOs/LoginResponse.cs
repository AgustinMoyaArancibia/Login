namespace Application.DTOs;

public record LoginResponse(
    int IdUsuario,
    string Nombre,
    string Apellido,
    DateTime? FechaLogin,
    string AccessToken
);
