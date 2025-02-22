namespace HotelBooking.Requests.Bookings;

public class SearchRoomRequest
{
    public string CityIdCode { get; set; }
    public int GuestNumber { get; set; }
    public DateOnly Start { get; set; }
    public DateOnly End { get; set; }
}