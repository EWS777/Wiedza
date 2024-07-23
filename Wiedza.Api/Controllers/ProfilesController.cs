using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemTextJsonPatch;
using Wiedza.Api.Core;
using Wiedza.Api.Core.Extensions;
using Wiedza.Core.Models;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Services;
using Administrator = Wiedza.Core.Models.Data.Administrator;

namespace Wiedza.Api.Controllers;

[ApiController, Route("[controller]")]
public class ProfilesController(
    IProfileService profileService
) : ControllerBase
{
    [HttpGet, Route("id/{UserId:guid}")]
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

    [HttpPatch, Route("my"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<ActionResult<Profile>> UpdateProfile([FromBody] JsonPatchDocument<Profile> updateProfile)
    {
        var userId = User.Claims.GetUserId();

        var updateResult = await profileService.UpdateProfileAsync(userId, updateProfile.ApplyTo);

        return updateResult.Match(profile => profile, e => throw e);
    }

    [HttpDelete, Route("delete"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<IActionResult> DeleteProfile([FromBody] string passwordHash)
    {
        var userId = User.Claims.GetUserId();

        var result = await profileService.DeleteProfileAsync(userId, passwordHash);

        return result.Match(_ => Ok("Profile was deleted!"), e => throw e);
    }

    [HttpPost, Route("verification"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<ActionResult<Verification>> VerifyProfile()
    {
        throw new NotImplementedException();
    }

    [HttpPost, Route("{username}/add-review"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<ActionResult<Review>> AddReview(string username, AddReviewRequest addReviewRequest)
    {
        var userId = User.Claims.GetUserId();
        var profileResult = await profileService.GetProfileAsync(username);
        if (profileResult.IsFailed) return NotFound("Person is not exist!");
        return await profileService.AddReviewAsync(profileResult.Value.PersonId, userId, addReviewRequest);
    }

    [HttpGet, Route("{username}/reviews"), Authorize(Policy = Policies.AdminPolicy)]
    public async Task<ActionResult<Review[]>> GetReviews(string username)
    {
        var profileResult = await profileService.GetProfileAsync(username);
        if (profileResult.IsFailed) return NotFound("Person is not exist!");
        return await profileService.GetReviewsAsync(profileResult.Value.PersonId);
    }
    
    
    [HttpGet, Route("admin/{UserId:guid}"), Authorize(Policy = Policies.AdminPolicy)]
    public async Task<ActionResult<Administrator>> GetAdministrator(Guid adminId)
    {
        var profileResult = await profileService.GetAdministratorAsync(adminId);
        return profileResult.Match(profile => profile, e => throw e);
    }
}