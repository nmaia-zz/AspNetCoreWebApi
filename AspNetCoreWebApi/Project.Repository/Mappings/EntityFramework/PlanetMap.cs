using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Entities;

namespace Project.Repository.Mappings.EntityFramework
{
    public class PlanetMap : IEntityTypeConfiguration<Planet>
    {
        public void Configure(EntityTypeBuilder<Planet> builder)
        {
            builder.ToTable("Planets");

            builder.Property(p => p.Id);

            builder.Property(p => p.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(p => p.Climate)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(p => p.Terrain)
                .HasColumnType("varchar(250)")
                .IsRequired();
        }
    }
}
