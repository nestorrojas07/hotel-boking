using Domain.Models.Reservation;

namespace HotelBooking.Requests.Bookings;

public class CreateBookingGuestRequest
{
    public string FullName { get; set; }
    public string Genre { get; set; }
    public string DocumentType { get; set; }
    public string DocumentId { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public bool? IsPrincipal { get; set; }

    public static implicit operator Guest(CreateBookingGuestRequest bookingRequest)
    {
        return new Guest()
        {
            FullName = bookingRequest.FullName,
            Email = bookingRequest.Email,
            Genre = bookingRequest.Genre,
            DocumentId = bookingRequest.DocumentId,
            DocumentType = bookingRequest.DocumentType,
            IsPrincipal = bookingRequest.IsPrincipal,
            Phone = bookingRequest.Phone,
        };
    }
}