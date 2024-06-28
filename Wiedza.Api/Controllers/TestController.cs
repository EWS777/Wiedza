using Microsoft.AspNetCore.Mvc;
using Wiedza.Api.Data;

namespace Wiedza.Api.Controllers;

[ApiController, Route("[controller]")]
public class TestController(DataContext dataContext) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        dataContext.Migrate();
        return Ok(dataContext.Database.ProviderName ?? "Empty");
    }
}