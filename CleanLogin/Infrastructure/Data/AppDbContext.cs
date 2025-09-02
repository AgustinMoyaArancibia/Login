using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<Usuario> Usuarios => Set<Usuario>();

        protected override void OnModelCreating(ModelBuilder model)
        {

            model.Entity<Usuario>(e =>
            {
                e.ToTable("Usuarios");
                e.HasKey(u => u.IdUsuario);
                e.Property(u => u.Nombre).IsRequired().HasMaxLength(50);
                e.Property(u => u.Apellido).IsRequired().HasMaxLength(50);
                e.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);
                e.Property(u => u.FechaLogin).IsRequired(false).HasColumnType("datetime2");
                e.HasIndex(u => u.Nombre).IsUnique();
            });

        }



    }
}
