using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemTextJsonPatch;
using Wiedza.Api.Core;
using Wiedza.Api.Core.Extensions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Controllers;

[ApiController, Route("[controller]"), Authorize(Policy = Policies.PersonPolicy)]
public class OffersController(
    IOfferService offerService
    ) : ControllerBase
{
    [HttpPut, Route("addService")]
    public async Task<ActionResult<Offer>> AddOfferToService([FromQuery] ulong publicationId, [FromQuery] string? message)
    {
        var userId = User.Claims.GetUserId();
        var result = await offerService.AddOfferToServiceAsync(userId, publicationId, message);
        return result.Match(offer => offer, e => throw e);
    }
    [HttpPut, Route("addProject")]
    public async Task<ActionResult<Offer>> AddOfferToProject([FromQuery] ulong publicationId, [FromQuery] string? message)
    {
        var userId = User.Claims.GetUserId();
        var result = await offerService.AddOfferToProjectAsync(userId, publicationId, message);
        return result.Match(offer => offer, e => throw e);
    }

    [HttpGet, Route("{offerId}")]
    public async Task<Result<Offer>> GetOffer(Guid offerId)
    {
        var userId = User.Claims.GetUserId();
        return await offerService.GetOfferAsync(userId, offerId);
    }

    [HttpGet, Route("all")]
    public async Task<ActionResult<Offer[]>> GetListOffer([FromQuery] ulong postId)
    {
        var userId = User.Claims.GetUserId();
        return await offerService.GetOfferListAsync(userId, postId);
    }

    [HttpPatch]
    public async Task<ActionResult<Offer>> UpdateOfferStatus(Guid offerId,
        [FromBody] JsonPatchDocument<UpdateOfferStatusRequest> update)
    {
        var userId = User.Claims.GetUserId();
        var result = await offerService.UpdateOfferStatusAsync(userId, offerId, update.ApplyTo);
        return result.Match(res => res, e => throw e);
    }
}