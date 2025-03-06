using Domain.Dtos.Citites;
using Domain.Models.Location;
using Domain.Ports.Repositories.Locations;

namespace Services.Cities;

public class CityService
{
    private  readonly ICityRepository _cityRepository;

    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public async Task<City?> GetById(long id)
    {
        return await _cityRepository.GetById(id);
    }

    public async Task<City?> GetByAbbreviation(string abbreviation)
    {
        return await _cityRepository.GetByAbbreviation(abbreviation);
    }

    public async Task<List<City>> GetAll()
    {
        return await _cityRepository.GetAll();
    }

    public async Task<City> Create(CityCreateRequest request)
    {
        City city = new City()
        {
            Abbreviation = request.Abbreviation,
            Name = request.Name,
        };
        return await _cityRepository.Create(city);
    }
}