namespace Domain.Models.Reservation;

public class Guest
{
    public long Id { get; set; }
    public long BookingId { get; set; }
    public string FullName { get; set; }
    public string Genre { get; set; }
    public string DocumentType { get; set; }
    public string DocumentId { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public bool? IsPrincipal { get; set; }
    public long CreatedBy{ get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}