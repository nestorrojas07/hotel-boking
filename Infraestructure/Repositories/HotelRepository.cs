using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos.Hotels;
using Domain.Models.HotelModels;
using Domain.Ports.Repositories.Hotels;
using Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly BookingContext _bookingContext;

    public HotelRepository(BookingContext bookingContext)
    {
        _bookingContext = bookingContext;
    }

    public async Task<Hotel> Create(Hotel hotel)
    {
        var response = await _bookingContext.Hotels.AddAsync(hotel);
        await _bookingContext.SaveChangesAsync();
        
        return response.Entity;
    }

    public async Task<Hotel?> GetHotelById(long id)
    {
        return await _bookingContext.Hotels.FirstOrDefaultAsync(x =>  x.Id == id);
    }

    public async Task<List<Hotel>> GetHotels()
    {
        return await _bookingContext.Hotels.ToListAsync();
    }

    public async Task<Hotel> Update(long hotelId, UpdateHotelRequest request)
    {
        Hotel hotel = await _bookingContext.Hotels.FirstAsync( x => x.Id == hotelId);
        hotel.UpdatedAt = DateTime.UtcNow;
        hotel.Name = request.Name;
        hotel.Description = request.Description;
        hotel.Starts = request.Starts;
        hotel.IsActive = request.IsActive;

        _bookingContext.Entry(hotel).State = EntityState.Modified;
        await _bookingContext.SaveChangesAsync();

        return hotel;

    }

    public async Task UpdateState(long hotelId, bool isActive)
    {
        Hotel hotel = await _bookingContext.Hotels.FirstAsync(x => x.Id == hotelId);
        hotel.UpdatedAt = DateTime.UtcNow;
        hotel.IsActive = isActive;

        _bookingContext.Entry(hotel).State = EntityState.Modified;
        await _bookingContext.SaveChangesAsync();
    }
}
