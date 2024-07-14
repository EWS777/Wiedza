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
    [HttpPut]
    [Authorize]
    public async Task<ActionResult<Publication>> AddPublication([FromBody] AddPublicationRequest addPublicationRequest)
    {
        var userId = User.Claims.GetUserId();

        var addResult = await publicationService.AddPublicationAsync(userId, addPublicationRequest);
        return addResult.Match(post => post, e => throw e);
    }

    [HttpPatch, Route("{publicationId:guid}")]
    [Authorize]
    public async Task<ActionResult<Publication>> ModifyPublication(Guid publicationId, [FromBody] JsonPatchDocument<Publication> publication)
    {
        var userId = User.Claims.GetUserId();

        var result = await publicationService.ModifyPublicationAsync(userId, publicationId, publication.ApplyTo);
        return result.Match(post => post, e => throw e);
    }

    [HttpGet, Route("{publicationId:guid}")]
    public async Task<ActionResult<Publication>> GetPublication(Guid publicationId)
    {
        var publicationResult = await publicationService.GetPublicationAsync(publicationId);
        return publicationResult.Match(profile => profile, e => throw e);
    }
    
    [HttpGet, Route("publications")]
    public async Task<ActionResult<List<Publication>>> GetPublication([FromBody] string type)
    {
        var publicationResult = await publicationService.GetPublicationAsync(type);
        return publicationResult.Match(result => result, e => throw e);
    }

    [HttpDelete, Route("delete")]
    [Authorize]
    public async Task<IActionResult> DeletePublication([FromBody]Guid publicationId)
    {
        var userId = User.Claims.GetUserId();
        var deletePublication = await publicationService.DeletePublicationAsync(userId, publicationId);
        return deletePublication.Match(_=>Ok("Publication was deleted"), e => throw e);
    }
}