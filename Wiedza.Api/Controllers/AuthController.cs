using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wiedza.Api.Core.Extensions;
using Wiedza.Api.Services;
using Wiedza.Core.Requests;
using Wiedza.Core.Responses;
using Wiedza.Core.Services;

namespace Wiedza.Api.Controllers;

[ApiController, Route("[controller]")]
public class AuthController(
    IAuthService authService,
    ExceptionHandlerService exceptionHandlerService
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



    [HttpPut, Route("change-password"), Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
    {
        var userId = User.Claims.GetUserId();
        var updatePassword = await authService.ChangePasswordAsync(userId, changePasswordRequest);
        return updatePassword.Match(_ => Ok("Password was changed!"), exception => exceptionHandlerService.HandleException(exception, HttpContext));
    }
}