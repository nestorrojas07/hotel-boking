using Domain.Models.Reservation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.EntityConfigurations.Reservation;

public class GuestEntityTypeConfiguration : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        builder.ToTable("guests");

        builder.HasKey(x => x.Id);

        builder.Property(p => p.BookingId)
            .HasColumnName("booking_id")
            .IsRequired();
        
        builder.Property(p => p.FullName)
            .HasColumnName("full_name")
            .IsRequired()
            .HasMaxLength(150);
        
        builder.Property(p => p.Genre)
            .HasColumnName("genre")
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(p => p.DocumentType)
            .HasColumnName("document_type")
            .IsRequired()
            .HasMaxLength(10);
        
        builder.Property(p => p.DocumentId)
            .HasColumnName("document_id")
            .IsRequired()
            .HasMaxLength(60);
        
        builder.Property(p => p.Email)
            .HasColumnName("email")
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(p => p.Phone)
            .HasColumnName("phone")
            .IsRequired(false)
            .HasMaxLength(20);

        builder.Property(p => p.IsPrincipal)
            .HasColumnName("is_principal")
            .IsRequired(false)
            .HasDefaultValue(false);
        
        builder.Property(p => p.CreatedBy)
            .HasColumnName("created_by")
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();
        
        builder.HasOne<Booking>()
            .WithMany()
            .HasForeignKey(h => h.BookingId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}