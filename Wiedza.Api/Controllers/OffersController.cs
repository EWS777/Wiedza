using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wiedza.Api.Core;
using Wiedza.Api.Core.Extensions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Services;

namespace Wiedza.Api.Controllers;

[ApiController, Route("[controller]"), Authorize(Policy = Policies.PersonPolicy)]
public class OffersController(
    IOfferService offerService
) : ControllerBase
{
    [HttpGet, Route("received")]
    public async Task<ActionResult<Offer[]>> GetReceivedOffers([FromQuery] ulong publicationId)
    {
        var userId = User.Claims.GetUserId();
        var result = await offerService.GetReceivedOffersAsync(userId, publicationId);
        return result.Match(offers => offers, e => throw e);
    }

    //endpoint to return my sent offers list to some post
    [HttpGet, Route("sended")]
    public async Task<ActionResult<Offer[]>> GetSendedOffers()
    {
        var userId = User.Claims.GetUserId();
        return await offerService.GetSendedOffersAsync(userId);
    }

    [HttpGet, Route("{offerId:guid}")]
    public async Task<ActionResult<Offer>> GetOffer(Guid offerId)
    {
        var userId = User.Claims.GetUserId();
        var result = await offerService.GetOfferAsync(userId, offerId);
        return result.Match(offer => offer, e => throw e);
    }

    [HttpPost, Route("add")]
    public async Task<ActionResult<Offer>> AddOfferToPublication([FromQuery] ulong publicationId,
        [FromQuery] string? message)
    {
        var userId = User.Claims.GetUserId();
        var result = await offerService.AddOfferToPublicationAsync(userId, publicationId, message);
        return result.Match(offer => offer, e => throw e);
    }

    [HttpPost, Route("{offerId:guid}/respond")]
    public async Task<ActionResult<Offer>> RespondToOffer(Guid offerId, [FromQuery] bool isApprove)
    {
        var userId = User.Claims.GetUserId();
        var result = await offerService.RespondToOfferAsync(userId, offerId, isApprove);
        return result.Match(offer => offer, e => throw e);
    }

    [HttpPost, Route("{offerId:guid}/change-status")]
    public async Task<ActionResult<Offer>> ChangeOfferStatus(Guid offerId, [FromQuery] bool isCompleted)
    {
        var userId = User.Claims.GetUserId();
        var result = await offerService.ChangeOfferStatusAsync(userId, offerId, isCompleted);
        return result.Match(offer => offer, e => throw e);
    }

    [HttpDelete, Route("{offerId:guid}")]
    public async Task<IActionResult> DeleteOffer(Guid offerId)
    {
        var userId = User.Claims.GetUserId();
        var result = await offerService.DeleteOfferAsync(userId, offerId);

        return result.Match(_ => Ok("Offer was deleted!"), e => throw e);
    }
}