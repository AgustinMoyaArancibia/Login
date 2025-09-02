namespace Application.DTOs;

public record CreateUsuarioResponse(
    int IdUsuario,
    string Nombre,
    string Apellido
);
