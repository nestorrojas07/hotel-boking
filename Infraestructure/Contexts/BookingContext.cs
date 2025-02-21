using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Auth;
using Domain.Models.HotelModels;
using Domain.Models.Location;
using Infraestructure.EntityConfigurations.Auth;
using Infraestructure.EntityConfigurations.Hotels;
using Infraestructure.EntityConfigurations.Location;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Contexts;

public class BookingContext : DbContext
{

    public DbSet<UserApp> Users { get; set; }

    public DbSet<City> Cities { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public BookingContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        modelBuilder.Entity<UserApp>();

        modelBuilder.ApplyConfiguration(new CityEntityTypeConfiguration());
        modelBuilder.Entity<City>();
        modelBuilder.ApplyConfiguration(new HotelEntityTypeConfiguration());
        modelBuilder.Entity<Hotel>();
        modelBuilder.ApplyConfiguration(new RoomEntityTypeConfiguration());
        modelBuilder.Entity<Room>();
    }

}
