using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos.Hotels;
using Domain.Models.HotelModels;

namespace Domain.Ports.Repositories.Hotels;

public interface IHotelRepository
{
    public Task<Hotel?> GetHotelById(long id);
    public Task<List<Hotel>> GetHotels();

    public Task<Hotel> Create(Hotel hotel);
    public Task<Hotel> Update(long hotelId, UpdateHotelRequest hotel);
    public Task UpdateState(long hotelId, bool isActive);
}
