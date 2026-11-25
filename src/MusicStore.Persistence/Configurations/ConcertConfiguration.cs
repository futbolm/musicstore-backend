using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStore.Entities;

namespace MusicStore.Persistence.Configurations;

public class ConcertConfiguration : IEntityTypeConfiguration<Concert>
{
    public void Configure(EntityTypeBuilder<Concert> builder)
    {
        builder.Property(x => x.Title).HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(200);
        builder.Property(x => x.Place).HasMaxLength(100);
        builder.Property(x => x.DateEvent)
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()");
        builder.Property(x => x.ImageUrl)
            .HasMaxLength(500)
            .IsUnicode(false);
        builder.HasIndex(x => x.Title); // añadimos un indice para el campo Title
        builder.ToTable("Concert", "MusicStore"); // creamos un esquema
    }
}
