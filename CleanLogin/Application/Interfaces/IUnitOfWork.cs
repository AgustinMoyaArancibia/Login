namespace Application.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken ct = default);
    //necesito un metodo para guardar los cambios en la base de datos
}
