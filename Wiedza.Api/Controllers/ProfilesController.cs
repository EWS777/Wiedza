using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemTextJsonPatch;
using Wiedza.Api.Core.Extensions;
using Wiedza.Api.Services;
using Wiedza.Core.Models;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
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
        return profileResult.Match(profile => profile, exceptionHandlerService.HandleException<Profile>);
    }

    [HttpGet, Route("{username}")]
    public async Task<ActionResult<Profile>> GetProfile(string username)
    {
        var profileResult = await profileService.GetProfileAsync(username);

        return profileResult.Match(profile => profile, exceptionHandlerService.HandleException<Profile>);
    }

    [HttpPatch, Route("my")]
    public async Task<ActionResult<Profile>> UpdateProfile([FromBody] JsonPatchDocument<Profile> updateProfile)
    {
        var userId = User.Claims.GetUserId();

        var updateResult = await profileService.UpdateProfileAsync(userId, updateProfile.ApplyTo);

        return updateResult.Match(profile => profile, exceptionHandlerService.HandleException<Profile>);
    }

    [HttpPut, Route("change-password")]
    public async Task<ActionResult<Person>> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
    {
        var userId = User.Claims.GetUserId();
        Console.WriteLine($"Received userId: {userId}");
        Console.WriteLine($"Received oldPassword: {changePasswordRequest.oldPassword}, newPassword: {changePasswordRequest.newPassword}");
        var updatePassword = await profileService.ChangePasswordAsync(userId, changePasswordRequest);
        return updatePassword.Match(profile => profile, exceptionHandlerService.HandleException<Person>);
    }
}