using Domain.Dtos.Hotels;
using FluentValidation;

namespace HotelBooking.Validators;

public class UpdateHotelRequestValidator : AbstractValidator<UpdateHotelRequest>
{
    public UpdateHotelRequestValidator()
    {
        RuleFor(p => p.Name)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(150)
                .WithMessage("Hotel's Name must be empty or greater than 150 characters");
        RuleFor(p => p.Description)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(500)
            .WithMessage("Hotel's Description must be empty or greater than 500 characters");
        RuleFor(p => p.Starts)
            .NotNull()
            .GreaterThan(0)
            .LessThanOrEqualTo(5)
            .WithMessage("Start at least one or max 5");
    }
}
