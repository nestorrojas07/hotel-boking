using Domain.Dtos.Hotels;
using Domain.Exceptions;
using Domain.Models.HotelModels;
using Domain.Ports.Repositories.Hotels;
using Infraestructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace Services.Hotels;

public class RoomServices 
{
    private readonly IRoomRepository _roomRepository;
    private readonly IHotelRepository _hotelRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RoomServices(IRoomRepository roomRepository, IHotelRepository hotelRepository)
    {
        _roomRepository = roomRepository;
        _hotelRepository = hotelRepository;
    }

    public async Task<Room?> GetRoomById(long id)
    {
        return await _roomRepository.GetRoomById(id);
    }

    public async Task<List<Room>> GetRoomsByHotel(long hotelId)
    {
        return await _roomRepository.GetRoomsByHotel(hotelId);
    }

    public async Task<Room> Create(long hotelId, CreateRoomRequest request)
    {
        var hotel = await _hotelRepository.GetHotelById(hotelId);
        
        if(hotel == null) throw new KeyNotFoundException($"Hotel {hotelId} not found");
        if (!hotel.IsActive) throw new DomainException("Hotel is not active"); 
        
        var user = _httpContextAccessor.HttpContext.User.CurrentUser();

        var room = new Room()
        {
            HotelId = hotelId,
            Description = request.Description,
            IsActive = request.IsActive,
            PriceBase = request.PriceBase,
            TaxPercentaje = request.TaxPercentaje,
            Name = request.Name,
            CreatedBy = user.Id,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        }; 
        return await _roomRepository.Create(room);
    }

    public async Task<Room> Update(long roomId, UpdateRoomRequest request)
    {
        return await _roomRepository.Update(roomId, request);
    }

    public async Task UpdateState(long roomId, bool isActive)
    {
        await _roomRepository.UpdateState(roomId, isActive);
    }
}