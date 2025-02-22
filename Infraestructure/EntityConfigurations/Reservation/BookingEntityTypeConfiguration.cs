using Domain.Models.HotelModels;
using Domain.Models.Location;
using Domain.Models.Reservation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.EntityConfigurations.Reservation;

public class BookingEntityTypeConfiguration  : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("booking");

        builder.HasKey(x => x.Id);

        builder.Property(p => p.Code)
            .HasColumnName("code")
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.StartAt)
            .HasColumnName("start_at")
            .IsRequired();
        
        builder.Property(p => p.EndAt)
            .HasColumnName("end_at")
            .IsRequired();
        
        builder.Property(p => p.CheckInAt)
            .HasColumnName("checkin_at")
            .IsRequired(false);

        builder.Property(p => p.EmergencyContactName)
            .HasColumnName("emergency_contact_name")
            .IsRequired(false)
            .HasMaxLength(150);
        
        builder.Property(p => p.EmergencyContactInfo)
            .HasColumnName("emergency_contact_info")
            .IsRequired(false)
            .HasMaxLength(200);
        

        builder.Property(p => p.HotelId)
            .HasColumnName("hotel_id")
            .IsRequired();
        
        builder.Property(p => p.RoomId)
            .HasColumnName("room_id")
            .IsRequired();
        
        builder.Property(p => p.GuestNumber)
            .HasColumnName("guest_number")
            .IsRequired();
        
        builder.Property(p => p.CityId)
            .HasColumnName("city_id")
            .IsRequired();
        
        builder.Property(p => p.State)
            .HasColumnName("state")
            .IsRequired();
        
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
            .HasForeignKey(h => h.HotelId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<Room>()
            .WithMany()
            .HasForeignKey(h => h.RoomId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<City>()
            .WithMany()
            .HasForeignKey(h => h.CityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}