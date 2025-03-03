using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HotelBooking.Middlewares;

public class FluentValidatorFilterAsync<T> : IAsyncActionFilter where T : class
{
    private readonly IValidator<T> _validator;

    public FluentValidatorFilterAsync(IValidator<T> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments.ContainsKey("request")) // Ajusta el nombre del argumento según tu controlador
        {
            var user = context.ActionArguments["request"] as T;

            if (user != null)
            {
                ValidationResult results = await _validator.ValidateAsync(user);

                if (!results.IsValid)
                {
                    context.Result = new BadRequestObjectResult(results.ToDictionary());
                    return;
                }
            }
        }

        await next();
    }
}
