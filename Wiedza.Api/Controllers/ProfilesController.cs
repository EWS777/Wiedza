using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemTextJsonPatch;
using Wiedza.Api.Core.Extensions;
using Wiedza.Core.Models;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Services;

namespace Wiedza.Api.Controllers;

[ApiController, Route("[controller]"), Authorize]
public class ProfilesController(
    IProfileService profileService,
    IAuthService authService
) : ControllerBase
{
    [HttpGet, Route("id/{personId:guid}")]
    public async Task<ActionResult<Profile>> GetProfile(Guid personId)
    {
        var profileResult = await profileService.GetProfileAsync(personId);
        return profileResult.Match(profile => profile, e => throw e);
    }

    [HttpGet, Route("{username}")]
    public async Task<ActionResult<Profile>> GetProfile(string username)
    {
        var profileResult = await profileService.GetProfileAsync(username);

        return profileResult.Match(profile => profile, e => throw e);
    }

    [HttpPatch, Route("my")]
    public async Task<ActionResult<Profile>> UpdateProfile([FromBody] JsonPatchDocument<Profile> updateProfile)
    {
        var userId = User.Claims.GetUserId();

        var updateResult = await profileService.UpdateProfileAsync(userId, updateProfile.ApplyTo);

        return updateResult.Match(profile => profile, e => throw e);
    }

    [HttpPatch, Route("delete")]
    public async Task<IActionResult> DeleteProfile([FromBody] string passwordHash)
    {
        var userId = User.Claims.GetUserId();

        var deleteResult = await authService.DeleteProfileAsync(userId, passwordHash);
        return deleteResult.Match(_ => Ok("Profile was deleted!"), e => throw e);
    }

    [HttpPost, Route("verification/")]
    public async Task<ActionResult<Verification>> VerifyProfile([FromBody] VerifyProfileRequest verifyProfileRequest)
    {
        var userId = User.Claims.GetUserId();
        var verify = await authService.VerifyProfileAsync(userId, verifyProfileRequest);
        return verify.Match(_ => Ok("Verification was created!"), e => throw e);
    }
}