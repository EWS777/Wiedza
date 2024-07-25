using Microsoft.AspNetCore.Authorization;
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
public class ServicesController(IServiceService serviceService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Service[]>> GetActiveServices()
    {
        var result= await serviceService.GetServicesAsync();
        foreach (var services in result)
        {
            services.Administrator = null;
            services.AdministratorId = null;
        }

        return result;
    }

    [HttpGet, Route("my"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<ActionResult<Service[]>> GetPersonServices()
    {
        var userId = User.Claims.GetUserId();
        var result = await serviceService.GetPersonServicesAsync(userId);
        foreach (var services in result)
        {
            services.Administrator = null;
            services.AdministratorId = null;
        }

        return result;
    }

    [HttpGet, Route("all"), Authorize(Policy = Policies.AdminPolicy)]
    public async Task<ActionResult<Service[]>> GetAllServices()
    {
        return await serviceService.GetServicesAsync(false);
    }

    [HttpGet, Route("{serviceId}")]
    public async Task<Result<Service>> GetService(ulong serviceId)
    {
        var result = await serviceService.GetServiceAsync(serviceId);
        result.Value.Administrator = null;
        result.Value.AdministratorId = null;
        return result;
    }

    [HttpPost, Route("add"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<ActionResult<Service>> AddService(AddPublicationRequest addPublicationRequest)
    {
        var userId = User.Claims.GetUserId();
        return await serviceService.AddServiceAsync(userId, addPublicationRequest);
    }

    [HttpPatch, Route("{serviceId}"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<ActionResult<Service>> UpdateService(ulong serviceId, [FromBody] JsonPatchDocument<UpdatePublicationRequest> update)
    {
        var userId = User.Claims.GetUserId();
        var result = await serviceService.UpdateService(userId, serviceId, update.ApplyTo);
        return result.Match(res => res, e => throw e);
    }

    [HttpDelete, Route("{serviceId}"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<IActionResult> DeleteService(ulong serviceId)
    {
        var userId = User.Claims.GetUserId();
        var result = await serviceService.DeleteServiceAsync(userId, serviceId);
        return result.Match(_ => Ok("Publication was deleted!"), e => throw e);
    }
}