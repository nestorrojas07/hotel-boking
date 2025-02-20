using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums.Auth;
using Domain.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.EntityConfigurations.Auth;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserApp>
{
    public void Configure(EntityTypeBuilder<UserApp> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);

        builder.Property(p => p.Email)
            .HasColumnName("email")
            .IsRequired()
            .HasMaxLength(150);
        builder.HasIndex(p => p.Email)
            .IsUnique();

        builder.Property(p => p.Password)
            .HasColumnName("password")
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.FullName)
            .HasColumnName("fullname")
            .IsRequired();

        builder.Property(p => p.Role)
            .HasColumnName("role")
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(p => p.IsActive)
            .HasColumnName("is_active")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

    }
}
