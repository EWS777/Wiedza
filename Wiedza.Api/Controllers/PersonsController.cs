using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wiedza.Api.Core;
using Wiedza.Api.Core.Extensions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;
using Wiedza.Core.Services;

namespace Wiedza.Api.Controllers;

[ApiController, Route("[controller]"), Authorize(Policy = Policies.AdminPolicy)]
public class PersonsController(
    IProfileService profileService) : ControllerBase
{
    [HttpGet, Route("all")]
    public async Task<ActionResult<Person[]>> GetPersons()
    {
        return await profileService.GetPersonsAsync();
    }

    [HttpPost, Route("{personId}/modify")]
    public async Task<ActionResult<Person>> UpdatePersonStatus([FromQuery] Guid personId, [FromQuery] AccountState status)
    {
        var adminId = User.Claims.GetUserId();
        var result = await profileService.UpdatePersonStatusAsync(personId, status, adminId);
        return result.Match(x => x, e => throw e);
    }
}