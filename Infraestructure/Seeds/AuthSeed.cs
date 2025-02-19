using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Auth;
using Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Seeds;

public static class AuthSeed
{
    public static void SeedUsers(this AuthContext authContext, IServiceProvider sp) {
        if (!authContext.Users.Any())
        {
            authContext.AddRange(
                new UserApp
                {
                    Id = 1,
                    Email = "admin@smarttalent.com",
                    Password = "password",
                    FullName = "admin",
                    IsActive = true,
                    Role = Domain.Enums.Auth.AppRole.Admin,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,

                },
                new UserApp
                {
                    Id = 2,
                    Email = "nestor@mail.com",
                    Password = "password",
                    FullName = "nestor",
                    IsActive = true,
                    Role = Domain.Enums.Auth.AppRole.Customer,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,

                }
            );
            authContext.SaveChanges();
        }
    }
}
