using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemTextJsonPatch;
using Wiedza.Api.Core.Extensions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Controllers;

[ApiController, Route("[controller]")]
public class ProjectsController(
    IProjectService projectService
    ) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Project[]>> GetActiveProjects()
    {
        return await projectService.GetProjectsAsync();
    }

    [HttpGet, Route("my"), Authorize]
    public async Task<ActionResult<Project[]>> GetPersonProjects()
    {
        var userId = User.Claims.GetUserId();
        return await projectService.GetPersonProjectsAsync(userId);
    }

    [HttpGet, Route("all"), Authorize] // TODO: Only admin
    public async Task<ActionResult<Project[]>> GetAllProjects()
    {
        return await projectService.GetProjectsAsync(false);
    }

    [HttpGet, Route("{projectId}")]
    public async Task<Result<Project>> GetProject(ulong projectId)
    {
        return await projectService.GetProjectAsync(projectId);
    }

    [HttpPut, Route("add"), Authorize]
    public async Task<ActionResult<Project>> AddProject(AddPublicationRequest addPublicationRequest)
    {
        var userId = User.Claims.GetUserId();
        return await projectService.AddProjectAsync(userId, addPublicationRequest);
    }

    [HttpPatch, Route("{projectId}/modify"), Authorize]
    public async Task<ActionResult<Project>> UpdateProject(ulong projectId, [FromBody] JsonPatchDocument<UpdatePublicationRequest> update)
    {
        var userId = User.Claims.GetUserId();
        var result = await projectService.UpdateProject(userId, projectId, update.ApplyTo);
        return result.Match(res => res, e => throw e);
    }

    [HttpDelete, Route("delete"), Authorize]
    public async Task<IActionResult> DeleteProject([FromQuery] ulong projectId)
    {
        var userId = User.Claims.GetUserId();
        var result = await projectService.DeleteProjectAsync(userId, projectId);
        return result.Match(_=>Ok("Publication was deleted!"), e => throw e);
    }
}