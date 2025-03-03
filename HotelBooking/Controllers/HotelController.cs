using Domain.Dtos.Hotels;
using Domain.Enums.Auth;
using Domain.Models.HotelModels;
using HotelBooking.Middlewares;
using HotelBooking.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Hotels;

namespace HotelBooking.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class HotelController : ControllerBase
{
    private readonly HotelServices _hotelServices;

    public HotelController(HotelServices hotelServices)
    {
        _hotelServices = hotelServices;
    }
    
    /// <summary>
    /// Add a new hotel to the system
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = $"{AppRole.Admin}")]
    [ServiceFilter(typeof(FluentValidatorFilterAsync<CreateHotelRequest>))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Hotel>> CreateHotel([FromBody] CreateHotelRequest request)
    {
        return Ok(await _hotelServices.Create(request));
    }
    
    /// <summary>
    /// Retreive a specific hotel by id
    /// </summary>
    /// <param name="hotelId"></param>
    /// <returns></returns>
    [HttpGet("{hotelId:long}")]
    [Authorize(Roles = $"{AppRole.Admin},{AppRole.Customer}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Hotel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Hotel>> GetHotel([FromRoute] long hotelId)
    {
        return Ok(await _hotelServices.GetHotelById(hotelId));
    }
    
    /// <summary>
    /// List all hotels
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = $"{AppRole.Admin},{AppRole.Customer}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Hotel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Hotel>> GetHotels()
    {
        return Ok(await _hotelServices.GetHotels());
    }
    
    /// <summary>
    /// Update data of the hotel
    /// </summary>
    /// <param name="hotelId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{hotelId:long}")]
    [Authorize(Roles = $"{AppRole.Admin}")]
    [ServiceFilter( typeof(FluentValidatorFilterAsync<UpdateHotelRequest>))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Hotel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Hotel>> UpdateHotel([FromRoute] long hotelId, [FromBody] UpdateHotelRequest request)
    {
        return Ok(await _hotelServices.Update(hotelId, request));
    }
    
    /// <summary>
    /// Enable or disable a specific hotel
    /// </summary>
    /// <param name="hotelId"></param>
    /// <param name="isActive"></param>
    /// <returns></returns>
    [HttpPut("{hotelId:long}/active/{isActive:bool}")]
    [Authorize(Roles = $"{AppRole.Admin}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> UpdateState([FromRoute] long hotelId, [FromRoute] bool isActive)
    {
        await _hotelServices.UpdateState(hotelId, isActive);
        
        return Ok();
    }
}