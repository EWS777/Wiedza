﻿using Microsoft.AspNetCore.Mvc;
using Wiedza.Core.Models;
using Wiedza.Core.Requests;
using Wiedza.Core.Responses;
using Wiedza.Core.Services;

namespace Wiedza.Api.Controllers;

[ApiController, Route("[controller]")]
public class ProfileController(IProfileService profileService) : ControllerBase
{
    [HttpGet("{idPerson}")]
    public async Task<ActionResult<EditProfileResponse>> GetEditProfile(Guid idPerson)
    {
        var result = await profileService.GetEditProfileAsync(idPerson);
        
        return result.Match<ActionResult<EditProfileResponse>>(response => response,
            exception => NotFound(exception.Message));
    }
}