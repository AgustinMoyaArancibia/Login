using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data;

// Esta fábrica la usan las Tools de EF en "design time" (migrations, update-database)
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // OJO: usá una cadena válida para tu SQL Server.
        // Podés dejar esta fija o leer de environment variables.
        var cs = "Server=AGUS\\SQLEXPRESS;Database=LoginDb;Trusted_Connection=True;TrustServerCertificate=True";


        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(cs)
            .Options;

        return new AppDbContext(options);
    }
}
