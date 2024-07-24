using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wiedza.Api.Core;
using Wiedza.Api.Core.Extensions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Services;

namespace Wiedza.Api.Controllers;

[ApiController, Route("[controller]")]
public class WithdrawsController(IWithdrawService withdrawService) : ControllerBase
{
    [HttpGet, Route("all"), Authorize(Policy = Policies.AdminPolicy)]
    public async Task<ActionResult<Withdraw[]>> GetWithdraws([FromQuery] Guid? personId = null)
    {
        if (personId is { } id) return await withdrawService.GetPersonWithdrawsAsync(id);
        return await withdrawService.GetAllWithdrawsAsync();
    }

    [HttpGet, Route("my"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<ActionResult<Withdraw[]>> GetPersonWithdraws()
    {
        var userId = User.Claims.GetUserId();
        var result = await withdrawService.GetPersonWithdrawsAsync(userId);

        foreach (var withdraw in result)
        {
            withdraw.Administrator = null;
            withdraw.AdministratorId = null;
        }

        return result;
    }

    [HttpGet, Route("{withdrawId:guid}"), Authorize(Policy = Policies.AdminAndPersonPolicy)]
    public async Task<ActionResult<Withdraw>> GetWithdraw(Guid withdrawId)
    {
        var userId = User.Claims.GetUserId();
        var role = User.Claims.GetRole();
        var result = await withdrawService.GetWithdrawAsync(withdrawId, userId);

        return result.Match(withdraw =>
        {
            if (role == Roles.AdministratorRole) return withdraw;

            withdraw.Administrator = null;
            withdraw.AdministratorId = null;
            return withdraw;
        }, e => throw e);
    }

    [HttpPost, Route("add"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<ActionResult<Withdraw>> AddWithdraw([FromBody] AddWithdrawRequest request)
    {
        var userId = User.Claims.GetUserId();
        var result = await withdrawService.AddWithdrawAsync(userId, request);
        return result.Match(withdraw => withdraw, e => throw e);
    }

    [HttpPost, Route("{withdrawId:guid}/change-status"), Authorize(Policy = Policies.AdminPolicy)]
    public async Task<ActionResult<Withdraw>> UpdateWithdrawStatus(Guid withdrawId, [FromQuery] bool isCompleted)
    {
        var userId = User.Claims.GetUserId();
        var result = await withdrawService.UpdateWithdrawStatusAsync(withdrawId, userId, isCompleted);
        return result.Match(withdraw => withdraw, e => throw e);
    }

    [HttpDelete, Route("{withdrawId:guid}"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<IActionResult> DeleteWithdraw(Guid withdrawId)
    {
        var userId = User.Claims.GetUserId();
        var result = await withdrawService.DeleteWithdrawAsync(withdrawId, userId);
        return result.Match(_ => Ok("Withdraw was deleted!"), e => throw e);
    }
}