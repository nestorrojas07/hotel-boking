using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Auth;
using Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infraestructure.Seeds;

public static class AuthSeed
{
    public static void SeedUsers(this BookingContext authContext, IServiceProvider sp, string passwordSalt= "") {
        if (!authContext.Users.Any())
        {
            authContext.AddRange(
                new UserApp
                {
                    Id = 1,
                    Email = "admin@smarttalent.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Password"),
                    FullName = "admin",
                    IsActive = true,
                    Role = nameof(Domain.Enums.Auth.AppRole.Admin),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                },
                new UserApp
                {
                    Id = 2,
                    Email = "nestor@mail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Password"),
                    FullName = "nestor",
                    IsActive = true,
                    Role = nameof(Domain.Enums.Auth.AppRole.Customer),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                }
            );
            authContext.SaveChanges();
        }
    }
}
