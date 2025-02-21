using Domain.Dtos.Hotels;
using Domain.Enums.Auth;
using Domain.Models.HotelModels;
using Domain.Ports.Repositories.Hotels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Hotels;

namespace HotelBooking.Controllers;

/// <summary>
/// Rooms manager controller
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class RoomController : ControllerBase
{
    private readonly RoomServices _roomServices;

    public RoomController(RoomServices roomServices)
    {
        _roomServices = roomServices;
    }
    
    /// <summary>
    ///  Add one room to the specific hotel
    /// </summary>
    /// <param name="hotelId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("hotel/{hotelId:long}/add-room")]
    [Authorize(Roles = $"{AppRole.Admin}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Room>> AddRoom([FromRoute] long hotelId,  [FromBody] CreateRoomRequest request)
    {
        return Ok(await _roomServices.Create(hotelId, request));
    }
    
    /// <summary>
    /// List all rooms by a specific hotel
    /// </summary>
    /// <param name="hotelId"></param>
    /// <returns></returns>
    [HttpGet("hotel/{hotelId:long}")]
    [Authorize(Roles = $"{AppRole.Admin},{AppRole.Customer}")]
    public async Task<ActionResult<List<Room>>> GetRooms([FromRoute] long hotelId)
    {
        return Ok(await _roomServices.GetRoomsByHotel(hotelId));
    }
    
    /// <summary>
    /// Update data from specific room
    /// </summary>
    /// <param name="roomId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{roomId:long}")]
    [Authorize(Roles = $"{AppRole.Admin}")]
    public async Task<ActionResult<Hotel>> UpdateRoom([FromRoute] long roomId, [FromBody] UpdateRoomRequest request)
    {
        return Ok(await _roomServices.Update(roomId, request));
    }
    
    /// <summary>
    /// Enable or disable a specific room
    /// </summary>
    /// <param name="roomId"></param>
    /// <param name="isActive"></param>
    /// <returns></returns>
    [HttpPut("{roomId:long}/active/{isActive:bool}")]
    [Authorize(Roles = $"{AppRole.Admin}")]
    public async Task<ActionResult> UpdateState([FromRoute] long roomId, [FromRoute] bool isActive)
    {
        await _roomServices.UpdateState(roomId, isActive);
        
        return Ok();
    }
    
}