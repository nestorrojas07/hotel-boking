using HotelBooking;
using Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Infraestructure;
using Infraestructure.Seeds;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPresentation()
    .AddInfraestructure(builder.Configuration);
    //.AddServices(builder.Configuration);

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

    var context = services.GetRequiredService<AuthContext>();
    context.Database.Migrate();
    context.SeedUsers(services);
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
