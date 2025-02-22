using Domain.Models.Location;

namespace Domain.Ports.Repositories.Locations;

public interface ICityRepository
{
    public Task<City?> GetById(long id);
    public Task<City?> GetByAbbreviation(string abbreviation);
    public Task<List<City>> GetAll();
    public Task<City> Create(City city);
}