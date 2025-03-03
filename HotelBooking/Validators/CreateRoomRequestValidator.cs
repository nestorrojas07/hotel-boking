using Domain.Dtos.Hotels;
using FluentValidation;

namespace HotelBooking.Validators;

public class CreateRoomRequestValidator : AbstractValidator<CreateRoomRequest>
{
    public CreateRoomRequestValidator()
    {
        RuleFor(p => p.Name)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(150)
                .WithMessage("Room's Name must be empty or greater than 150 characters");
        RuleFor(p => p.Description)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(500)
            .WithMessage("Room's Description must be empty or greater than 500 characters");
        RuleFor(p => p.PriceBase)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");

        RuleFor(p => p.TaxPercentaje)
            .NotNull()
            .GreaterThan(0)
            .LessThanOrEqualTo(1)
            .WithMessage("Tax must be between 0 and 1");
        RuleFor(p => p.GuestNumber)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("Guest number must be greater than 0");
    }
}
