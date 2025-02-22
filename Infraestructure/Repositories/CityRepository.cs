using Domain.Models.Location;
using Domain.Ports.Repositories.Locations;
using Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories;

public class CityRepository : ICityRepository
{
    private readonly BookingContext _bookingContext;

    public CityRepository(BookingContext bookingContext)
    {
        _bookingContext = bookingContext;
    }

    public async Task<City?> GetById(long id)
    {
       return await _bookingContext.Cities.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<City?> GetByAbbreviation(string abbreviation)
    {
        return await _bookingContext.Cities.FirstOrDefaultAsync(x => x.Abbreviation == abbreviation);
    }

    public async Task<List<City>> GetAll()
    {
        return await _bookingContext.Cities.OrderBy( x => x.Name).ToListAsync();
    }

    public async Task<City> Create(City city)
    {
        var response = await _bookingContext.Cities.AddAsync(city);
        await _bookingContext.SaveChangesAsync();

        return city;
    }
}