using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wiedza.Api.Core;
using Wiedza.Api.Core.Extensions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Services;

namespace Wiedza.Api.Controllers;

[ApiController, Route("[controller]")]
public class WithdrawsController(
    IWithdrawService withdrawService
    ) : ControllerBase
{
    [HttpGet, Authorize(Policy = $"{Policies.PersonPolicy},{Policies.AdminPolicy}")]
    public async Task<ActionResult<Withdraw>> GetWithdraw(Guid withdrawId)
    {
        var userId = User.Claims.GetUserId();
        var result = await withdrawService.GetWithdrawAsync(userId, withdrawId);
        return result.Match(withdraw => withdraw, e => throw e);
    }

    [HttpPost, Route("add"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<ActionResult<Withdraw>> AddWithdraw([FromQuery] float amount, [FromQuery] ulong cardNumber)
    {
        var userId = User.Claims.GetUserId();
        var result = await withdrawService.AddWithdrawAsync(userId, amount, cardNumber);
        return result.Match(withdraw => withdraw, e => throw e);
    }
    
    [HttpGet, Route("all"), Authorize(Policy = $"{Policies.PersonPolicy},{Policies.AdminPolicy}")]
    public async Task<ActionResult<Withdraw[]>> GetWithdraws()
    {
        var userId = User.Claims.GetUserId();
        var result = await withdrawService.GetWithdrawsAsync(userId);
        return result.Match(withdraws => withdraws, e => throw e);
    }
    
    [HttpGet, Route("{withdrawId}"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<IActionResult> DeleteWithdraw(Guid withdrawId)
    {
        var userId = User.Claims.GetUserId();
        var result = await withdrawService.DeleteWithdrawAsync(userId, withdrawId);
        return result.Match(_=>Ok("Withdraw was deleted!"), e => throw e);
    }

    [HttpPost, Route("{withdrawId}/change-status"), Authorize(Policy = Policies.AdminPolicy)]
    public async Task<ActionResult<Withdraw>> UpdateWithdrawStatus(Guid withdrawId, [FromQuery] bool isCompleted)
    {
        var userId = User.Claims.GetUserId();
        var result = await withdrawService.UpdateWithdrawStatusAsync(userId, withdrawId, isCompleted);
        return result.Match(withdraw => withdraw, e => throw e);
    }
}