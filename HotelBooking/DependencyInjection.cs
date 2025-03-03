using System.Reflection;
using Domain.Ports.CaseUse;
using Services.Auth.Options;
using Services.Auth;
using HotelBooking.Middlewares;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using Services.Bookings;
using Services.Hotels;
using FluentValidation;
using HotelBooking.Requests.Bookings;
using HotelBooking.Validators;
using Domain.Dtos.Hotels;

namespace HotelBooking;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });;
        services.AddEndpointsApiExplorer();
        services.AddHttpContextAccessor();

        services.AddSwaggerGen(options =>
        {
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, 
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });
        services.AddTransient<GloblalExceptionHandlingMiddleware>();

        services.AddAuthentication().AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        configuration.GetSection("Jwt:Secret").Value!))
            };
        });
        services.AddValidators();

        return services;
    }

    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddOptions<JwtOptions>()
                .BindConfiguration("Jwt")
                .ValidateDataAnnotations()
                .ValidateOnStart();
        services.AddOptions<AuthOption>()
                .BindConfiguration("Auth")
                .ValidateDataAnnotations()
                .ValidateOnStart();

        services.AddScoped<ITokenValidator, JtwTokenValidator>();
        services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
        services.AddScoped<LoginService>();
        services.AddScoped<HotelServices>();
        services.AddScoped<RoomServices>();
        services.AddScoped<BookingService>();


        return services;

    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateBookingRequest>, CreateBookingRequestValidator>();
        services.AddScoped<IValidator<CreateHotelRequest>, CreateHotelRequestValidator>();
        services.AddScoped<IValidator<CreateRoomRequest>, CreateRoomRequestValidator>();
        services.AddScoped<IValidator<UpdateHotelRequest>, UpdateHotelRequestValidator>();
        services.AddScoped<IValidator<UpdateRoomRequest>, UpdateRoomRequestValidator>();

        services.AddScoped(typeof(FluentValidatorFilterAsync<>));

        return services;
    }
}
