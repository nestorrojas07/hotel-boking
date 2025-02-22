using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Domain.Models.HotelModels;
using Domain.Models.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.EntityConfigurations.Hotels;

public class RoomEntityTypeConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("rooms");

        builder.HasKey(x => x.Id);

        builder.Property(p => p.HotelId)
            .HasColumnName("hotel_id")
            .IsRequired();
        
        builder.Property(p => p.CityId)
            .HasColumnName("city_id")
            .IsRequired();

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(150);

        builder.HasIndex(p => new { p.HotelId, p.Name })
             .IsUnique();
        
        builder.Property(p => p.GuestNumber)
            .HasColumnName("guest_number")
            .IsRequired()
            .HasDefaultValue(1);

        builder.Property(p => p.Description)
            .HasColumnName("description")
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.PriceBase)
            .HasColumnName("price_base")
            .IsRequired();

        builder.Property(p => p.TaxPercentaje)
            .HasColumnName("tax_percentaje")
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

        builder.HasOne<Hotel>()
            .WithMany()
            .HasForeignKey(x => x.HotelId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<City>(room => room.City)
            .WithMany()
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}
