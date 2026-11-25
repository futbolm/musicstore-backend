
using Microsoft.EntityFrameworkCore;
using MusicStore.Entities;
using MusicStore.Persistence.Projections;
using System.Reflection;

namespace MusicStore.Persistence;

public class ApplicationDbContext : DbContext  
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    // Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // para que aplique la configuracion 
        // modelBuilder.Entity<Genre>().Property(x => x.Name).HasMaxLength(50);

        // esto lo usamos para el projection
        modelBuilder.Entity<ConcertDetailView>(entity =>
        {
            entity.HasNoKey();
            entity.ToView(null);
        });

        modelBuilder.Entity<SaleReportView>(entity =>
        {
            entity.HasNoKey();
            entity.ToView(null); //Esto evita que EF la incluya en migraciones
        });

    }

    //public DbSet<Genre> Genres { get; set; } = default!;

}
 