using Domain.Models.HotelModels;
using Domain.Models.Reservation;

namespace Domain.Ports.Repositories.Bookings;

public interface IBookingRepository
{
    public Task<Booking> Create(Booking hotel);
    public Task<Booking> GetById(long id);
    public Task<Booking> GetByCode(string code);
    public Task AddGuests(long bookingId, IEnumerable<Guest> guests);
    public Task<IEnumerable<Guest>> GetGuests(long bookingId);
    public Task<bool> ChangeStatus(long bookingId, string newStatus);
    public Task<IEnumerable<Hotel>> GetHotels(DateTime startDate, DateTime endDate, int numberOfGuests, long cityId);
    public Task<IEnumerable<Room>> GetRooms(long hotelId, DateTime startDate, DateTime endDate, int numberOfGuests, long cityId);
}