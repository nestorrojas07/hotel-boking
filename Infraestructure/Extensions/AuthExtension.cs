using System.Security.Claims;
using Domain.Dtos;

namespace Infraestructure.Extensions;

public static class AuthExtension
{
    public static UserPrincipal CurrentUser(this ClaimsPrincipal principal) {

        foreach (var claim in principal.Claims)
        {
            Console.WriteLine($"{claim.Type}  =>   '{claim.Value}'");
        }
        var user = new UserPrincipal();
        user.Id = int.Parse(principal.Claims.First(i => i.Type == "userId").Value);
        user.Email = principal.Claims.First(i => i.Type == ClaimTypes.Email).Value;
        user.Role = principal.Claims.First(i => i.Type == ClaimTypes.Role).Value;
        user.FullName = principal.Claims.First(i => i.Type == ClaimTypes.Name).Value;

        return user;
    }
}
