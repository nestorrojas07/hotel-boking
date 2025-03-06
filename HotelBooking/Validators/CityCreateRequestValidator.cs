using Domain.Dtos.Citites;
using Domain.Ports.Repositories.Locations;
using FluentValidation;

namespace HotelBooking.Validators;

public class CityCreateRequestValidator: AbstractValidator<CityCreateRequest> 
{
    private readonly ICityRepository _cityRepository;
    
    public CityCreateRequestValidator(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
        
        
        RuleFor(p => p.Name)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(150)
            .WithMessage("Room's Name must be empty or greater than 150 characters");
        RuleFor(p => p.Abbreviation)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(10)
            .WithMessage("Room's Description must be empty or greater than 500 characters")
            .MustAsync(async (p, ct) =>
            {
                var city = await _cityRepository.GetByAbbreviation(p);
                return city == null;
            }).WithMessage("Already exists abbreviation");
    }
}