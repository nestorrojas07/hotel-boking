using Domain.Enums.Bookings;
using Domain.Models.Reservation;

namespace HotelBooking.Requests.Bookings;

public class CreateBookingRequest
{
    public DateOnly StartAt { get; set; }
    public DateOnly EndAt { get; set; }
    public string EmergencyContactName { get; set; }
    public string EmergencyContactInfo { get; set; }
    public long HotelId { get; set; }
    public long RoomId { get; set; }
    public int GuestNumber { get; set; }
    public long CityId { get; set; }
    
    public List<CreateBookingGuestRequest> Guests { get; set; }
    
    public static explicit operator Booking(CreateBookingRequest bookingRequest)
    {
        return new Booking()
        {
            HotelId = bookingRequest.HotelId,
            StartAt = bookingRequest.StartAt.ToDateTime(TimeOnly.FromTimeSpan(TimeSpan.Parse("14:00:00.000"))),
            EndAt = bookingRequest.EndAt.ToDateTime(TimeOnly.FromTimeSpan(TimeSpan.Parse("10:59:59.999"))),
            CityId = bookingRequest.CityId,
            GuestNumber = bookingRequest.GuestNumber,
            RoomId = bookingRequest.RoomId,
            EmergencyContactInfo = bookingRequest.EmergencyContactInfo,
            EmergencyContactName = bookingRequest.EmergencyContactName,
            State = BookingStates.Reserved,
            CreatedAt = DateTime.UtcNow,
        };
    }
}