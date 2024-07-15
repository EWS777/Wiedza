using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemTextJsonPatch;
using Wiedza.Api.Core.Extensions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Services;

namespace Wiedza.Api.Controllers;

[ApiController, Route("[controller]")]
public class PublicationsController(
    IPublicationService publicationService
    ) : ControllerBase
{
    [HttpGet, Route("{publicationId}")]
    public async Task<ActionResult<Publication>> GetPublication(ulong publicationId)
    {
        var publicationResult = await publicationService.GetPublicationAsync(publicationId);
        return publicationResult.Match(profile => profile, e => throw e);
    }

    [HttpGet]
    public async Task<ActionResult<Publication[]>> GetActivePublications([FromQuery] ulong? fromId = null,
        [FromQuery] int? limit = null, [FromQuery] bool? isProject = null)
    {
        if (fromId is null || limit is null)
        {
            return await publicationService.GetActivePublicationsAsync(isProject);
        }

        return await publicationService.GetActivePublicationsAsync(fromId.Value, limit.Value, isProject);
    }

    [HttpGet, Route("all")]
    public async Task<ActionResult<Publication[]>> GetAllPublications([FromQuery] ulong? fromId = null,
        [FromQuery] int? limit = null, [FromQuery] bool? isProject = null)
    {
        if (fromId is null || limit is null)
        {
            return await publicationService.GetPublicationsAsync(isProject);
        }

        return await publicationService.GetPublicationsAsync(fromId.Value, limit.Value, isProject);
    }

    [HttpPut, Authorize]
    public async Task<ActionResult<Publication>> AddPublication([FromBody] AddPublicationRequest addPublicationRequest)
    {
        var userId = User.Claims.GetUserId();

        var addResult = await publicationService.AddPublicationAsync(userId, addPublicationRequest);
        return addResult.Match(post => post, e => throw e);
    }

    [HttpPatch, Route("{publicationId}"), Authorize]
    public async Task<ActionResult<Publication>> ModifyPublication(ulong publicationId, [FromBody] JsonPatchDocument<PublicationUpdateRequest> update)
    {
        var userId = User.Claims.GetUserId();

        var result = await publicationService.UpdatePublicationAsync(userId, publicationId, update.ApplyTo);

        return result.Match(publication => publication, e => throw e);
    }


    [HttpDelete, Route("{publicationId}"), Authorize]
    public async Task<IActionResult> DeletePublication([FromQuery] ulong publicationId)
    {
        var userId = User.Claims.GetUserId();
        var deletePublication = await publicationService.DeletePublicationAsync(userId, publicationId);
        return deletePublication.Match(_ => Ok("Publication was deleted"), e => throw e);
    }
}