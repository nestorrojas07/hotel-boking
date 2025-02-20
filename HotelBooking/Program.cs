using HotelBooking;
using Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Infraestructure;
using Infraestructure.Seeds;
using Services.Auth.Options;
using Microsoft.Extensions.Options;
using HotelBooking.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPresentation(builder.Configuration)
    .AddInfraestructure(builder.Configuration)
    .AddAppServices(builder.Configuration);
//builder.Services.AddOptions<JwtOptions>().BindConfiguration("jwt").ValidateOnStart();

// builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var autopt = services.GetRequiredService<IOptions<AuthOption>>();
    
    var context = services.GetRequiredService<AuthContext>();
    context.Database.Migrate();
    context.SeedUsers(services, autopt.Value.PasswordSalt);
}



app.UseHttpsRedirection();
app.UseMiddleware<GloblalExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
