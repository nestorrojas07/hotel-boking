using System.Data.Common;
using Domain.Models.HotelModels;
using Domain.Models.Reservation;
using Domain.Ports.Repositories.Bookings;
using Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Npgsql;

namespace Infraestructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly BookingContext _bookingContext;


    public BookingRepository(BookingContext bookingContext)
    {
        _bookingContext = bookingContext;
    }

    public async Task<Booking> Create(Booking booking)
    {
        var response = await _bookingContext.Bookings.AddAsync(booking);
        await _bookingContext.SaveChangesAsync();

        return booking;
    }

    public async Task<Booking?> GetById(long id)
    {
        return await _bookingContext.Bookings.FirstOrDefaultAsync(x =>  x.Id == id);
    }

    public async Task<Booking?> GetByCode(string code)
    {
        return await _bookingContext.Bookings.FirstOrDefaultAsync(x =>  x.Code == code);
    }

    public async Task AddGuests(long bookingId, IEnumerable<Guest> guests)
    {
        await _bookingContext.Guests.AddRangeAsync(guests);
        await _bookingContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Guest>> GetGuests(long bookingId)
    {
        return await _bookingContext.Guests.Where(x => x.BookingId == bookingId).ToListAsync();
    }

    public async Task<bool> ChangeStatus(long bookingId, string newStatus)
    {
        _bookingContext.Bookings.Where(x => x.Id == bookingId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.State, newStatus)
                .SetProperty(b => b.UpdatedAt, DateTime.UtcNow));
        return true;
    }

    public async Task<IEnumerable<Hotel>> GetHotels(DateTime startDate, DateTime endDate, int numberOfGuests,
        long cityId)
    {
        
        var sql = """
                  select distinct h.* from hotels h
                  where h.city_id = @pCityId and exists (
                  	select 1 from rooms r
                  	where r.is_active = true
                  	and r.city_id = @pCityId
                  	and r.guest_number > 0 and r.guest_number <= @pNumberOfGuests
                  	and r.hotel_id = h."Id" 
                  	and not exists (
                  		select * from booking b 
                  			where b.room_id = r."Id"
                  			and b.start_at <= @pEndAt and b.end_at >= @pStartAt
                  			and b.state != 'Cancelled'
                  	)
                  )
                  """;
        _bookingContext.Hotels.Where(h => h.IsActive && h.CityId == cityId);
            
            
        var pNumberOfGuests = new NpgsqlParameter("pNumberOfGuests", numberOfGuests);
        var pStartAt = new NpgsqlParameter("pStartAt", startDate);
        var pEndAt = new NpgsqlParameter("pEndAt", endDate);
        var pCityId = new NpgsqlParameter("pCityId", cityId);
        return await _bookingContext.Hotels.FromSqlRaw(sql, pNumberOfGuests, pStartAt, pEndAt, pCityId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Room>> GetRooms(long hotelId, DateTime startDate, DateTime endDate,
        int numberOfGuests, long cityId)
    {
        var sql = """
                  select r.* from rooms r
                  where r.hotel_id= @pHotelId and r.is_active = true
                  and r.guest_number > 0 and r.guest_number <= @pNumberOfGuests
                  and r.city_id = @pCityId
                  and not exists (
                  	select 1 from booking b 
                  		where b.room_id = r."Id"
                  		and b.start_at <= @pEndAt and b.end_at >= @pStartAt
                  		and b.guest_number > 0 and b.guest_number <= @pNumberOfGuests
                  		and b.state != 'Cancelled'
                    )
                  """;
        var pHotelId = new NpgsqlParameter("pHotelId", hotelId);
        var pNumberOfGuests = new NpgsqlParameter("pNumberOfGuests", numberOfGuests);
        var pStartAt = new NpgsqlParameter("pStartAt", startDate);
        var pEndAt = new NpgsqlParameter("pEndAt", endDate);
        var pCityId = new NpgsqlParameter("pCityId", cityId);
        
        return await _bookingContext.Rooms.FromSqlRaw(sql,pHotelId,pNumberOfGuests, pStartAt, pEndAt, pCityId)
            .ToListAsync();
    }
}