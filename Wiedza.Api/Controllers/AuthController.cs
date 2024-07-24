using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wiedza.Api.Core.Extensions;
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

        return result.Match<ActionResult<LoginResponse>>(response => response, e => throw e);
    }

    [HttpPost, Route("register")]
    public async Task<ActionResult<LoginResponse>> Register([FromBody] RegisterRequest request)
    {
        var result = await authService.RegisterAsync(request);
        return result.Match<ActionResult<LoginResponse>>(response => response, e => throw e);
    }

    [HttpPost, Route("refresh")]
    public async Task<ActionResult<LoginResponse>> Refresh([FromHeader(Name = "Authorization")] string authorization)
    {
        var refreshResult = await authService.RefreshTokenAsync(authorization.Replace("Bearer ", ""));

        return refreshResult.Match(response => response, e => throw e);
    }

    [HttpPost, Route("change-password"), Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
    {
        var userId = User.Claims.GetUserId();
        var updatePassword = await authService.ChangePasswordAsync(userId, changePasswordRequest);
        return updatePassword.Match(_ => Ok("Password was changed!"), e => throw e);
    }
}