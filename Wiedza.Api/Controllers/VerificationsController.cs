using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wiedza.Api.Core;
using Wiedza.Api.Core.Extensions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Services;

namespace Wiedza.Api.Controllers;

[ApiController, Route("[controller]")]
public class VerificationsController(IVerificationService verificationService) : ControllerBase
{
    [HttpGet, Route("all"), Authorize(Policy = Policies.AdminPolicy)]
    public async Task<ActionResult<Verification[]>> GetAllVerifications()
    {
        return await verificationService.GetAllVerificationsAsync();
    }

    [HttpGet, Route("{verificationId:guid}"), Authorize(Policy = Policies.AdminAndPersonPolicy)]
    public async Task<ActionResult<Verification>> GetVerification(Guid verificationId)
    {
        var userId = User.Claims.GetUserId();
        var role = User.Claims.GetRole();
        var result = await verificationService.GetVerificationAsync(verificationId, userId);

        return result.Match(verification =>
        {
            if (role == Roles.AdministratorRole) return verification;

            verification.Administrator = null;
            verification.AdministratorId = null;
            return verification;
        }, e => throw e);
    }

    [HttpPost, Route("add"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<ActionResult<Verification>> AddVerification([FromBody] AddVerificationRequest request)
    {
        var userId = User.Claims.GetUserId();
        return await verificationService.AddVerificationAsync(userId, request);
    }

    [HttpPost, Route("{verificationId:guid}/change-status"), Authorize(Policy = Policies.AdminPolicy)]
    public async Task<ActionResult<Verification>> UpdateVerificationStatus(Guid verificationId, [FromQuery] bool isCompleted)
    {
        var userId = User.Claims.GetUserId();
        var result = await verificationService.UpdateVerificationStatusAsync(verificationId, userId, isCompleted);
        return result.Match(verification => verification, e => throw e);
    }

    [HttpDelete, Route("{verificationId:guid}"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<IActionResult> DeleteVerification(Guid verificationId)
    {
        var userId = User.Claims.GetUserId();
        var result = await verificationService.DeleteVerificationAsync(verificationId, userId);
        return result.Match(_ => Ok("Verification is deleted!"), e => throw e);
    }
}