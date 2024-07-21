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
public class ServicesController(IServiceService serviceService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Service[]>> GetActiveServices()
    {
        return await serviceService.GetServicesAsync();
    }

    [HttpGet, Route("my"), Authorize]
    public async Task<ActionResult<Service[]>> GetPersonServices()
    {
        var userId = User.Claims.GetUserId();
        return await serviceService.GetPersonServicesAsync(userId);
    }

    [HttpGet, Route("all"), Authorize] // TODO: Only admin
    public async Task<ActionResult<Service[]>> GetAllServices()
    {
        return await serviceService.GetServicesAsync(false);
    }

    [HttpGet, Route("{serviceId}")]
    public async Task<Result<Service>> GetService(ulong serviceId)
    {
        return await serviceService.GetServiceAsync(serviceId);
    }

    [HttpPut, Route("add"), Authorize]
    public async Task<ActionResult<Service>> AddService(AddPublicationRequest addPublicationRequest)
    {
        var userId = User.Claims.GetUserId();
        return await serviceService.AddServiceAsync(userId, addPublicationRequest);
    }

    [HttpPatch, Route("{serviceId}/modify"), Authorize]
    public async Task<ActionResult<Service>> UpdateService(ulong serviceId, [FromBody] JsonPatchDocument<UpdatePublicationRequest> update)
    {
        var userId = User.Claims.GetUserId();
        var result = await serviceService.UpdateService(userId, serviceId, update.ApplyTo);
        return result.Match(res => res, e => throw e);
    }

    [HttpDelete, Route("delete"), Authorize]
    public async Task<IActionResult> DeleteService([FromQuery] ulong serviceId)
    {
        var userId = User.Claims.GetUserId();
        var result = await serviceService.DeleteServiceAsync(userId, serviceId);
        return result.Match(_=>Ok("Publication was deleted!"), e => throw e);
    }
}