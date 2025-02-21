using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos.Hotels;
using Domain.Models.HotelModels;

namespace Domain.Ports.Repositories.Hotels;

public interface IRoomRepository
{
    public Task<Room?> GetRoomById(long id);
    public Task<List<Room>> GetRoomsByHotel(long hotelId);

    public Task<Room> Create(Room room);
    public Task<Room> Update(long roomId, UpdateRoomRequest request);
    public Task UpdateState(long roomId, bool isActive);
}
