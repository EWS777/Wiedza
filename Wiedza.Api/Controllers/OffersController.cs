using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wiedza.Api.Core.Extensions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Services;

namespace Wiedza.Api.Controllers;

[ApiController, Route("[controller]"), Authorize]
public class OffersController(
    IOfferService offerService
    ) : ControllerBase
{
    [HttpPut]
    public async Task<ActionResult<Offer>> AddOffer([FromQuery] ulong publicationId, [FromQuery] string? message)
    {
        var userId = User.Claims.GetUserId();
        var result = await offerService.AddOfferAsync(userId, publicationId, message);
        return result.Match(offer => offer, e => throw e);
    }
}