using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wiedza.Api.Core.Extensions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Services;

namespace Wiedza.Api.Controllers;

[ApiController, Route("[controller]"),Authorize]
public class PublicationsController(
    IPublicationService publicationService
    ) : ControllerBase
{
    [HttpPut]
    public async Task<ActionResult<Publication>> AddPublication([FromBody] AddPublicationRequest addPublicationRequest)
    {
        var userId = User.Claims.GetUserId();

        var addResult = await publicationService.AddPublicationAsync(userId, addPublicationRequest);
        return addResult.Match(post => post, e => throw e);
    }
}