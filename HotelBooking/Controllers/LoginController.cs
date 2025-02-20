using HotelBooking.Requests.Auth;
using HotelBooking.Responses;
using Microsoft.AspNetCore.Mvc;
using Services.Auth;

namespace HotelBooking.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class LoginController : ControllerBase
{
    private readonly LoginService _loginService;

    public LoginController(LoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost()]
    public async Task<ActionResult<DataResponse<string>>> Login([FromBody] LoginRequest request) {
        string token = await _loginService.Login(request.Email, request.Password);
        
        return Ok( new DataResponse<string>() { Data = token});
    }
}
