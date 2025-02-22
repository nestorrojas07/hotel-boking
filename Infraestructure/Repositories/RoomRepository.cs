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

public class RoomRepository : IRoomRepository
{
    private readonly BookingContext _bookingContext;

    public RoomRepository(BookingContext bookingContext)
    {
        _bookingContext = bookingContext;
    }

    public async Task<Room> Create(Room room)
    {
        await _bookingContext.Rooms.AddAsync(room);
        await _bookingContext.SaveChangesAsync();

        return room;
    }

    public async Task<Room?> GetRoomById(long id)
    {
        return await _bookingContext.Rooms.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Room?> GetRoomByNameIntoHotel(long hotelId, string name)
    {
        return await _bookingContext.Rooms.FirstOrDefaultAsync(r => r.HotelId == hotelId && r.Name == name);
    }

    public async Task<List<Room>> GetRoomsByHotel(long hotelId)
    {
        return await _bookingContext.Rooms.Where(x => x.HotelId == hotelId).ToListAsync();
    }

    public async Task<Room> Update(long roomId, UpdateRoomRequest request)
    {
        Room room = await _bookingContext.Rooms.FirstAsync(x => x.Id == roomId);
        room.UpdatedAt = DateTime.UtcNow;
        room.Name = request.Name;
        room.Description = request.Description;
        room.PriceBase = request.PriceBase;
        room.TaxPercentaje = request.TaxPercentaje;
        room.IsActive = request.IsActive;

        _bookingContext.Entry(room).State = EntityState.Modified;
        await _bookingContext.SaveChangesAsync();

        return room;
    }

    public async Task UpdateState(long roomId, bool isActive)
    {
        Room room = await _bookingContext.Rooms.FirstAsync(x => x.Id == roomId);
        room.UpdatedAt = DateTime.UtcNow;
        room.IsActive = isActive;

        _bookingContext.Entry(room).State = EntityState.Modified;
        await _bookingContext.SaveChangesAsync();
    }
}
