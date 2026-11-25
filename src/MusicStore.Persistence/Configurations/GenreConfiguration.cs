using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStore.Entities;

namespace MusicStore.Persistence.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(70); 
        builder.ToTable("Genre", "MusicStore"); // creamos un esquema 
        builder.HasQueryFilter(x => x.Status); // le decimos que status va a ser un campo bandera, todo lo que no cumplan esta condicion va ser filtrados , usando queryFilter
    }
}

