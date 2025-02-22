using Domain.Models.HotelModels;

namespace Domain.Models.Reservation;

public class Booking
{
    public long Id { get; set; }
    public string Code { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public DateTime? CheckInAt { get; set; }
    public string EmergencyContactName { get; set; }
    public string EmergencyContactInfo { get; set; }
    public long HotelId { get; set; }
    public long RoomId { get; set; }
    public int GuestNumber { get; set; }
    public long CityId { get; set; }
    public string State { get; set; }
    public long CreatedBy{ get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
}