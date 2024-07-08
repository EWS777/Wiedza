using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemTextJsonPatch;
using Wiedza.Api.Core.Extensions;
using Wiedza.Api.Services;
using Wiedza.Core.Models;
using Wiedza.Core.Services;

namespace Wiedza.Api.Controllers;

[ApiController, Route("[controller]"), Authorize]
public class ProfilesController(
    IProfileService profileService,
    ExceptionHandlerService exceptionHandlerService
) : ControllerBase
{
    [HttpGet, Route("id/{personId:guid}")]
    public async Task<ActionResult<Profile>> GetProfile(Guid personId)
    {
        var profileResult = await profileService.GetProfileAsync(personId);
        return profileResult.Match(profile => profile, e => exceptionHandlerService.HandleException<Profile>(e, HttpContext));
    }

    [HttpGet, Route("{username}")]
    public async Task<ActionResult<Profile>> GetProfile(string username)
    {
        var profileResult = await profileService.GetProfileAsync(username);

        return profileResult.Match(profile => profile, e => exceptionHandlerService.HandleException<Profile>(e, HttpContext));
    }

    [HttpPatch, Route("my")]
    public async Task<ActionResult<Profile>> UpdateProfile([FromBody] JsonPatchDocument<Profile> updateProfile)
    {
        var userId = User.Claims.GetUserId();

        var updateResult = await profileService.UpdateProfileAsync(userId, updateProfile.ApplyTo);

        return updateResult.Match(profile => profile, e => exceptionHandlerService.HandleException<Profile>(e, HttpContext));
    }
}