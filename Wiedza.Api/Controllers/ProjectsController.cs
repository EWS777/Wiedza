using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wiedza.Api.Core.Extensions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Services;

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
}