using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wiedza.Api.Core;
using Wiedza.Api.Core.Extensions;
using Wiedza.Core.Models.Data.Base;
using Wiedza.Core.Models.Enums;
using Wiedza.Core.Services;

namespace Wiedza.Api.Controllers;

[ApiController, Route("[controller]"), Authorize(Policy = Policies.AdminPolicy)]
public class PublicationsController(IPublicationService publicationService) : ControllerBase
{
    [HttpPost, Route("{publicationId}")]
    public async Task<ActionResult<Publication>> UpdatePublicationStatus([FromQuery] ulong publicationId, [FromQuery] PublicationStatus status)
    {
        var userId = User.Claims.GetUserId();
        var result = await publicationService.UpdatePublicationStatusAsync(publicationId, status, userId);
        return result.Match(e => e, e => throw e);
    }

    [HttpPost, Route("{publicationId}/delete")]
    public async Task<IActionResult> DeletePublication([FromQuery] ulong publicationId)
    {
        var result = await publicationService.DeletePublicationAsync(publicationId);
        return result.Match(_ => Ok("Publication was deleted!"), e => throw e);
    }
}