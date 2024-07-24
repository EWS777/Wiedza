using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wiedza.Api.Core;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Services;

namespace Wiedza.Api.Controllers;

[ApiController, Route("[controller]")]
public class StatisticsController : ControllerBase
{
    [HttpGet, Route("balance"), Authorize(Policy = Policies.AdminPolicy)]
    public async Task<ActionResult<WebsiteBalance>> GetBalance()
    {
        throw new NotImplementedException();
    }
}