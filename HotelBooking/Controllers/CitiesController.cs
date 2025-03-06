using Domain.Dtos.Citites;
using Domain.Dtos.Hotels;
using Domain.Enums.Auth;
using Domain.Models.Location;
using HotelBooking.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Cities;

namespace HotelBooking.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly CityService _cityService;

    public CitiesController(CityService cityService)
    {
        _cityService = cityService;
    }
    
    
    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<City?>> GetById(long id)
    {
        return Ok(await _cityService.GetById(id));
    }
    
    [HttpGet("abbreviation/{abbreviation}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<City?>> GetByAbbreviation(string abbreviation)
    {
        return Ok(await _cityService.GetByAbbreviation(abbreviation));
    }
    
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<City>>> GetAll()
    {
        return Ok(await _cityService.GetAll());
    }
    
    [HttpPost]
    [Authorize(Roles = $"{AppRole.Admin}")]
    [ServiceFilter(typeof(FluentValidatorFilterAsync<CityCreateRequest>))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<City>> Create(CityCreateRequest city)
    {
        return Ok(await _cityService.Create(city));
    }
}