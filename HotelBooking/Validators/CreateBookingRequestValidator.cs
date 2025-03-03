using FluentValidation;
using HotelBooking.Requests.Bookings;

namespace HotelBooking.Validators;

public class CreateBookingRequestValidator : AbstractValidator<CreateBookingRequest> 
{

    public CreateBookingRequestValidator()
    {
        RuleFor(p => p.StartAt)
            .NotEmpty()
            .LessThan( p => p.EndAt )
            .WithMessage("The booking date must start before the end of the booking");
        RuleFor(p => p.EndAt)
            .NotEmpty()
            .GreaterThan( p => p.StartAt )
            .WithMessage("The booking date must start after the end of the booking");
        RuleFor(p => p.EmergencyContactName)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(150)
            .WithMessage("The emergency contact Name must be between 1 and 150 characters long.");
        RuleFor(p => p.EmergencyContactInfo)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(200)
            .WithMessage("The emergency contact information must be between 1 and 200 characters long.");
        RuleFor(p => p.RoomId)
            .NotNull()
            .GreaterThan(1)
            .WithMessage("The room is required");
        RuleFor(p => p.GuestNumber)
            .NotNull()
            .GreaterThan(1)
            .WithMessage("Guest number are required.");
        RuleFor(p => p.CityId)
            .NotNull()
            .GreaterThan(1)
            .WithMessage("The City is required");
    }

}
