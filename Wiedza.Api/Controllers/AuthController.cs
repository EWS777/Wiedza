using Microsoft.AspNetCore.Mvc;
using Wiedza.Core.Requests;
using Wiedza.Core.Responses;
using Wiedza.Core.Services;

namespace Wiedza.Api.Controllers;

[ApiController, Route("[controller]")]
public class AuthController(
    IAuthService authService
        ) : ControllerBase
{
    [HttpPost, Route("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var result = await authService.LoginAsync(request);

        return result.Match<ActionResult<LoginResponse>>(response => response,
            exception => Unauthorized(exception.Message));
    }

    [HttpPost, Route("register")]
    public async Task<ActionResult<LoginResponse>> Register([FromBody] RegisterRequest request)
    {
        var result = await authService.RegisterAsync(request);
        return result.Match<ActionResult<LoginResponse>>(response => response,
            exception => BadRequest(exception.Message));
    }
}