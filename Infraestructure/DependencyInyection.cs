﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Ports.Repositories.Auth;
using Domain.Ports.Repositories.Bookings;
using Domain.Ports.Repositories.Hotels;
using Domain.Ports.Repositories.Locations;
using Infraestructure.Contexts;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure;

public static class DependencyInyection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddRepositories();

        return services;

    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BookingContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("AuthDb"));
        });

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserAppRepository, UserAppRepository>();
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();

        return services;
    }
}
