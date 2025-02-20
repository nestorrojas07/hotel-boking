using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Auth;
using Infraestructure.EntityConfigurations.Auth;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Contexts;

public class AuthContext : DbContext
{

    public DbSet<UserApp> Users { get; set; }
    public AuthContext(DbContextOptions options) : base(options)
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
    }

}
