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

    /// <summary>
    /// Generate Jwt Token via user and password
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResponse<string>))]
    public async Task<ActionResult<DataResponse<string>>> Login([FromBody] LoginRequest request) {
        string token = await _loginService.Login(request.Email, request.Password);
        
        return Ok( new DataResponse<string>() { Data = token});
    }
}
