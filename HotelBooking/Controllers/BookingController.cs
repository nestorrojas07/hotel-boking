using Domain.Dtos.Hotels;
using Domain.Enums.Auth;
using Domain.Enums.Bookings;
using Domain.Exceptions;
using Domain.Models.HotelModels;
using Domain.Models.Reservation;
using HotelBooking.Middlewares;
using HotelBooking.Requests.Bookings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Bookings;

namespace HotelBooking.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BookingController : ControllerBase
{
    private readonly BookingService _bookingService;

    public BookingController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }
    
    /// <summary>
    /// Search for available hotels, in a range of dates and a number of people.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("search-hotels")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Hotel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<Hotel>>> SearchHotel([FromQuery] SearchRoomRequest request)
    {
        return Ok(await _bookingService.GetHotels(request.Start, request.End, request.GuestNumber, request.CityIdCode));
    }
    
    /// <summary>
    /// Get available hotel rooms in a date range
    /// </summary>
    /// <param name="hotelId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("hotel/{hotelId:long}/get-free-rooms")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Room>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Room>> SearchRoomsByHotel([FromRoute] long hotelId, [FromQuery] SearchRoomRequest request)
    {
        return Ok(await _bookingService.GetRooms(hotelId, request.Start, request.End, request.GuestNumber, request.CityIdCode));
    }
    
    /// <summary>
    /// Get Booking detail for a bookingcode
    /// </summary>
    /// <param name="bookingCode"></param>
    /// <returns></returns>
    [HttpGet("{bookingCode}")]
    [Authorize(Roles = $"{AppRole.Admin}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Booking))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Booking>> GetBookingByCode([FromRoute] string bookingCode)
    {
        return Ok(await _bookingService.GetByCode(bookingCode));
    }
    
    /// <summary>
    /// Get extra info "Gests" for a Booking Code
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    [HttpGet("get-reservation-detail/{code}/guests")]
    [Authorize(Roles = $"{AppRole.Admin}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Booking))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Booking>> GetGuestes([FromRoute] string code)
    {
        return Ok(await _bookingService.GetGuests(code));
    }
    
    /// <summary>
    /// Create a new booking
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="DomainException"></exception>
    [HttpPost("register-reservation")]
    [Authorize(Roles = $"{AppRole.Admin},{AppRole.Customer}")]
    [ServiceFilter(typeof(FluentValidatorFilterAsync<CreateBookingRequest>))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Booking>> Register([FromBody] CreateBookingRequest request)
    {
        if (request.GuestNumber != request.Guests.Count)
            throw new DomainException($"you might need to register all of guest info {request.Guests.Count} of {request.GuestNumber}.");
        
        Booking booking = (Booking)request;
        booking = await _bookingService.Create((Booking)request);
        List<Guest> guests = request.Guests.Select(g => (Guest)g).ToList();
        await _bookingService.AddGuests(booking.Id, guests);
        
        return Ok(booking);
    }
    
    /// <summary>
    /// Update booking status for checkin
    /// </summary>
    /// <param name="bookingCode"></param>
    /// <returns></returns>
    [HttpPut("checkin/{bookingCode}")]
    [Authorize(Roles = $"{AppRole.Admin}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> CheckIn([FromRoute] string bookingCode)
    {
        await _bookingService.ChangeStatus(bookingCode, BookingStates.CheckIn);
        
        return Ok();
    }
    
    /// <summary>
    /// Change booking status for Cancel
    /// </summary>
    /// <param name="bookingCode"></param>
    /// <returns></returns>
    [HttpPut("cancel/{bookingCode}")]
    [Authorize(Roles = $"{AppRole.Admin}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Cancel([FromRoute] string bookingCode)
    {
        await _bookingService.ChangeStatus(bookingCode, BookingStates.Cancelled);
        
        return Ok();
    }
    
    /// <summary>
    /// Change booking status for checkout
    /// </summary>
    /// <param name="bookingCode"></param>
    /// <returns></returns>
    [HttpPut("checkout/{bookingCode}")]
    [Authorize(Roles = $"{AppRole.Admin}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Checkout([FromRoute] string bookingCode)
    {
        await _bookingService.ChangeStatus(bookingCode, BookingStates.CheckOut);
        
        return Ok();
    }
}