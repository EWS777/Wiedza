﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemTextJsonPatch;
using Wiedza.Api.Core;
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
        var result = await projectService.GetProjectsAsync();

        foreach (var projects in result)
        {
            projects.Administrator = null;
            projects.AdministratorId = null;
        }

        return result;
    }

    [HttpGet, Route("my"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<ActionResult<Project[]>> GetPersonProjects()
    {
        var userId = User.Claims.GetUserId();
        var result = await projectService.GetPersonProjectsAsync(userId);
        
        foreach (var projects in result)
        {
            projects.Administrator = null;
            projects.AdministratorId = null;
        }

        return result;
    }

    [HttpGet, Route("all"), Authorize(Policy = Policies.AdminPolicy)]
    public async Task<ActionResult<Project[]>> GetAllProjects()
    {
        return await projectService.GetProjectsAsync(false);
    }

    [HttpGet, Route("{projectId}")]
    public async Task<Result<Project>> GetProject(ulong projectId)
    {
        var result = await projectService.GetProjectAsync(projectId);
        result.Value.Administrator = null;
        result.Value.AdministratorId = null;
        return result;
    }

    [HttpPost, Route("add"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<ActionResult<Project>> AddProject(AddPublicationRequest addPublicationRequest)
    {
        var userId = User.Claims.GetUserId();
        return await projectService.AddProjectAsync(userId, addPublicationRequest);
    }

    [HttpPatch, Route("{projectId}"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<ActionResult<Project>> UpdateProject(ulong projectId,
        [FromBody] JsonPatchDocument<UpdatePublicationRequest> update)
    {
        var userId = User.Claims.GetUserId();
        var result = await projectService.UpdateProject(userId, projectId, update.ApplyTo);
        return result.Match(res => res, e => throw e);
    }

    [HttpDelete, Route("{projectId}"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<IActionResult> DeleteProject([FromRoute] ulong projectId)
    {
        var userId = User.Claims.GetUserId();
        var result = await projectService.DeleteProjectAsync(userId, projectId);
        return result.Match(_ => Ok("Publication was deleted!"), e => throw e);
    }
}