using Domain.Dtos.Hotels;
using Domain.Ports.Repositories.Locations;
using FluentValidation;

namespace HotelBooking.Validators
{
    public class CreateHotelRequestValidator : AbstractValidator<CreateHotelRequest>
    {

        public CreateHotelRequestValidator(ICityRepository cityRepository)
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
            RuleFor(p => p.CityId)
                .GreaterThan(0)
                .MustAsync( async (id, ct) => {
                        var city = await cityRepository.GetById(id);
                        return  city!=null;
                    })
                .WithMessage("City is Required");
        }
    }
}
