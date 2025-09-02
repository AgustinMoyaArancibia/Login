using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _db;
    public UsuarioRepository(AppDbContext db) => _db = db;

    public Task<Usuario?> GetByNombreAsync(string nombre, CancellationToken ct = default)
        => _db.Usuarios.FirstOrDefaultAsync(u => u.Nombre == nombre, ct);

    public async Task AddAsync(Usuario user, CancellationToken ct = default)          // ⬅️ nuevo
        => await _db.Usuarios.AddAsync(user, ct);
}
