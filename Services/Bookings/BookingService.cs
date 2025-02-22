using Domain.Exceptions;
using Domain.Models.HotelModels;
using Domain.Models.Reservation;
using Domain.Ports.Repositories.Bookings;
using Domain.Ports.Repositories.Locations;
using Infraestructure.Contexts;
using Infraestructure.Extensions;
using Infraestructure.Repositories;
using Microsoft.AspNetCore.Http;

namespace Services.Bookings;

public class BookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private static TimeSpan checkintAt = TimeSpan.Parse("10:59:59.999");
    private static TimeSpan checkoutAt = TimeSpan.Parse("14:00:00.000");


    public BookingService(IBookingRepository bookingRepository, ICityRepository cityRepository, IHttpContextAccessor httpContextAccessor)
    {
        _bookingRepository = bookingRepository;
        _cityRepository = cityRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Booking> Create(Booking booking)
    {
        var user = _httpContextAccessor.HttpContext.User.CurrentUser();
        booking.CreatedBy = user.Id;
        booking.Code = Guid.NewGuid().ToString("N")[..-10];
        booking.CreatedAt = DateTime.UtcNow;
        booking.UpdatedAt = DateTime.UtcNow;
        return await _bookingRepository.Create(booking);
    }

    public async Task<Booking> GetById(long id)
    {
        return await _bookingRepository.GetById(id);
    }

    public async Task<Booking> GetByCode(string code)
    {
        return await _bookingRepository.GetByCode(code);
    }

    public async Task AddGuests(long bookingId, IEnumerable<Guest> guests)
    {
        await _bookingRepository.AddGuests(bookingId, guests);
    }

    public async Task<IEnumerable<Guest>> GetGuests(long bookingId)
    {
        return await _bookingRepository.GetGuests(bookingId);
    }
    
    public async Task<IEnumerable<Guest>> GetGuests(string bookingCode)
    {
        var booking = await _bookingRepository.GetByCode(bookingCode);
        if (booking == null)
            throw new KeyNotFoundException($" booking code {bookingCode} not found");
        
        return await _bookingRepository.GetGuests(booking.Id);
    }

    public async Task<bool> ChangeStatus(string bookingCode, string newStatus)
    {
        var booking = await _bookingRepository.GetByCode(bookingCode);
        if (booking == null)
            throw new KeyNotFoundException($" booking code {bookingCode} not found");
        return await _bookingRepository.ChangeStatus(booking.Id, newStatus);
    }

    public async Task<IEnumerable<Hotel>> GetHotels(DateOnly startDate, DateOnly endDate, int numberOfGuests, string cityCode)
    {
        var city = await _cityRepository.GetByAbbreviation(cityCode);
        if (city == null) throw new DomainException("Invalid city");
        DateTime startAt = startDate.ToDateTime(TimeOnly.FromTimeSpan(checkoutAt + TimeSpan.FromMinutes(1)));
        DateTime endAt = startDate.ToDateTime(TimeOnly.FromTimeSpan(checkintAt -  TimeSpan.FromMinutes(1)));
        
        return await _bookingRepository.GetHotels(startAt, endAt, numberOfGuests, city.Id);
    }

    public async Task<IEnumerable<Room>> GetRooms(long hotelId, DateOnly startDate, DateOnly endDate, int numberOfGuests, string cityCode)
    {
        var city = await _cityRepository.GetByAbbreviation(cityCode);
        if (city == null) throw new DomainException("Invalid city");
        
        DateTime startAt = startDate.ToDateTime(TimeOnly.FromTimeSpan(checkoutAt + TimeSpan.FromMinutes(1)));
        DateTime endAt = startDate.ToDateTime(TimeOnly.FromTimeSpan(checkintAt -  TimeSpan.FromMinutes(1)));
        
        return await _bookingRepository.GetRooms(hotelId, startAt, endAt, numberOfGuests, city.Id);
    }
}