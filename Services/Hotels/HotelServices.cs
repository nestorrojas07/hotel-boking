using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos.Hotels;
using Domain.Models.HotelModels;
using Domain.Ports.Repositories.Hotels;
using Infraestructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace Services.Hotels;

public class HotelServices
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HotelServices(IHotelRepository hotelRepository, IHttpContextAccessor httpContextAccessor)
    {
        _hotelRepository = hotelRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Hotel?> GetHotelById(long id)
    {
        return await _hotelRepository.GetHotelById(id);
    }

    public async Task<List<Hotel>> GetHotels()
    {
        return await _hotelRepository.GetHotels();
    }

    public async Task<Hotel> Create(CreateHotelRequest request)
    {
        var user = _httpContextAccessor.HttpContext.User.CurrentUser();
        var hotel = new Hotel()
        {
            Name = request.Name,
            Description = request.Description,
            Starts = request.Starts,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedBy = user.Id,
            IsActive = true,
        };
        
        return await _hotelRepository.Create(hotel);
    }

    public async Task<Hotel> Update(long hotelId, UpdateHotelRequest hotel)
    {
        return await _hotelRepository.Update(hotelId, hotel);
    }

    public async Task UpdateState(long hotelId, bool isActive)
    {
        await _hotelRepository.UpdateState(hotelId, isActive);
    }
}
