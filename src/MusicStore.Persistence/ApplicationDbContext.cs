
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicStore.Entities;
using MusicStore.Persistence.Projections;
using System.Reflection;

namespace MusicStore.Persistence;

public class ApplicationDbContext : IdentityDbContext<MusicStoreUserIdentity> //DbContext  
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

        // esto se agrego cuando entramos a seguridad
        modelBuilder.Entity<MusicStoreUserIdentity>(x => x.ToTable("Users"));
        modelBuilder.Entity<IdentityRole>(x => x.ToTable("Roles"));
        modelBuilder.Entity<IdentityUserRole<string>>(x => x.ToTable("UserRoles"));
        modelBuilder.Entity<IdentityRoleClaim<string>>(x => x.ToTable("RoleClaims"));
        modelBuilder.Entity<IdentityUserClaim<string>>(x => x.ToTable("UserClaims"));
        modelBuilder.Entity<IdentityUserLogin<string>>(x => x.ToTable("UserLogins"));
        modelBuilder.Entity<IdentityUserToken<string>>(x => x.ToTable("UserTokens"));

    }

    //public DbSet<Genre> Genres { get; set; } = default!;

}
 