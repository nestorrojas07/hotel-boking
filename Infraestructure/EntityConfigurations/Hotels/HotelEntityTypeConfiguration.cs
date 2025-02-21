using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.HotelModels;
using Domain.Models.Location;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.EntityConfigurations.Hotels;

public class HotelEntityTypeConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Hotel> builder)
    {
        builder.ToTable("hotels");

        builder.HasKey(x => x.Id);

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.Description)
            .HasColumnName("description")
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.Starts)
            .HasColumnName("starts")
            .IsRequired();
        

        builder.Property(p => p.IsActive)
            .HasColumnName("is_active")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(p => p.CreatedBy)
            .HasColumnName("created_by")
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.HasMany<Room>( hotel => hotel.Rooms)
            .WithOne( x => x.Hotel )
            .HasForeignKey(x => x.HotelId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<City>(hotel => hotel.City)
            .WithMany()
            .HasForeignKey(h => h.CityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
