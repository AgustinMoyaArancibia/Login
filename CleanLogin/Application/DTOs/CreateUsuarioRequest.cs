namespace Application.DTOs;

public record CreateUsuarioRequest(
    string Nombre,
    string Apellido,
    string Password
);
