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
    
    //endpoint to return offers list to some post
    [HttpGet, Route("received")]
    public async Task<ActionResult<Offer[]>> GetReceivedOfferList([FromQuery] ulong postId)
    {
        var userId = User.Claims.GetUserId();
        return await offerService.GetReceivedOfferListAsync(userId, postId);
    }
    
    //endpoint to return my sent offers list to some post
    [HttpGet, Route("sent")]
    public async Task<ActionResult<Offer[]>> GetSentOfferList()
    {
        var userId = User.Claims.GetUserId();
        return await offerService.GetSentOfferListAsync(userId);
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