using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class RegisterService : IRegisterService
{
    private readonly IUsuarioRepository _repo;
    private readonly IPasswordHasher _hasher;
    private readonly IUnitOfWork _uow;

    public RegisterService(IUsuarioRepository repo, IPasswordHasher hasher, IUnitOfWork uow)
    {
        _repo = repo; _hasher = hasher; _uow = uow;
    }

    public async Task<CreateUsuarioResponse> RegisterAsync(CreateUsuarioRequest request, CancellationToken ct = default)
    {
        // Chequeo de unicidad por Nombre
        var exists = await _repo.GetByNombreAsync(request.Nombre, ct);
        if (exists is not null)
            throw new InvalidOperationException("El nombre de usuario ya existe");

        // Hash de contraseña
        var hash = _hasher.Hash(request.Password);

        var user = new Usuario
        {
            Nombre = request.Nombre,
            Apellido = request.Apellido,
            PasswordHash = hash,
            FechaLogin = null
        };

        await _repo.AddAsync(user, ct);
        await _uow.SaveChangesAsync(ct);

        return new CreateUsuarioResponse(user.IdUsuario, user.Nombre, user.Apellido);
    }
}
