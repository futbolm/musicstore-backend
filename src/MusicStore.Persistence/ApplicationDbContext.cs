
using Microsoft.EntityFrameworkCore;
using MusicStore.Entities;
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

    }

    //public DbSet<Genre> Genres { get; set; } = default!;

}
 