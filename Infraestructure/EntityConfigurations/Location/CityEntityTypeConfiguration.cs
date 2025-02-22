using Domain.Models.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.EntityConfigurations.Location;

public class CityEntityTypeConfiguration   : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("cities");

        builder.HasKey(x => x.Id);

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(150);
        
        builder.Property(p => p.Abbreviation)
            .HasColumnName("abbreviation")
            .IsRequired()
            .HasMaxLength(10);
        
        builder.HasIndex(p => p.Abbreviation)
            .IsUnique();
        
    }
    
}