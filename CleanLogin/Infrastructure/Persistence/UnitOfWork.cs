using Application.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;
    public UnitOfWork(AppDbContext db) => _db = db;

    public Task SaveChangesAsync(CancellationToken ct = default)
        => _db.SaveChangesAsync(ct);
}
