using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wiedza.Api.Core;
using Wiedza.Api.Core.Extensions;
using Wiedza.Api.Repositories;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Controllers;

[ApiController, Route("[controller]")]
public class ComplaintsController(
    IComplaintService complaintService,
    IProfileService profileService,
    IPublicationRepository publicationRepository
    ): ControllerBase
{
    [HttpPost, Route("{username}/complaint"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<ActionResult<PersonComplaint>> AddPersonComplaint(string username,
        AddComplaintRequest addComplaintRequest)
    {
        var authorId = User.Claims.GetUserId();
        var profileResult = await profileService.GetProfileAsync(username);
        if (profileResult.IsFailed) return NotFound("Person is not exist!");
        return await complaintService.AddPersonComplaintAsync(authorId, profileResult.Value.PersonId, addComplaintRequest);
    }
    
    [HttpPost, Route("{publicationId}/publication-complaint"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<ActionResult<PublicationComplaint>> AddPublicationComplaint(ulong publicationId,
        AddComplaintRequest addComplaintRequest)
    {
        var authorId = User.Claims.GetUserId();
        var publicationResult = await publicationRepository.GetPublicationAsync(publicationId);
        if (publicationResult.IsFailed) return NotFound("Publication is not exist!");
        return await complaintService.AddPublicationComplaintAsync(authorId, publicationResult.Value.Id, addComplaintRequest);
    }

    [HttpGet, Route("/all-user"), Authorize(Policy = Policies.AdminPolicy)]
    public async Task<ActionResult<PersonComplaint[]>> GetPersonComplaints()
    {
        return await complaintService.GetPersonComplaintsAsync();
    }
    
    [HttpGet, Route("user/{personComplaintId}"), Authorize(Policy = Policies.AdminPolicy)]
    public async Task<Result<PersonComplaint>> GetPersonComplaint(Guid personComplaintId)
    {
        return await complaintService.GetPersonComplaintAsync(personComplaintId);
    }
    
    [HttpGet, Route("/all-publication"), Authorize(Policy = Policies.AdminPolicy)]
    public async Task<PublicationComplaint[]> GetPublicationComplaints()
    {
        return await complaintService.GetPublicationComplaintsAsync();
    }

    [HttpGet, Route("publication/{publicationComplaintId}"), Authorize(Policy = Policies.AdminPolicy)]
    public async Task<Result<PublicationComplaint>> GetPublicationComplaint(Guid publicationComplaintId)
    {
        return await complaintService.GetPublicationComplaintAsync(publicationComplaintId);
    }

    [HttpPost, Route("{personComplaintId}/modify-user"), Authorize(Policy = Policies.AdminPolicy)]
    public async Task<Result<PersonComplaint>> ModifyPersonComplaint(Guid personComplaintId, [FromQuery]bool isCompleted)
    {
        var adminId = User.Claims.GetUserId();
        return await complaintService.ModifyPersonComplaintAsync(adminId, personComplaintId, isCompleted);
    }
    
    [HttpPost, Route("{publicationComplaintId}/modify-publication"), Authorize(Policy = Policies.AdminPolicy)]
    public async Task<Result<PublicationComplaint>> ModifyPublicationComplaint(Guid publicationComplaintId, [FromQuery]bool isCompleted)
    {
        var adminId = User.Claims.GetUserId();
        return await complaintService.ModifyPublicationComplaintAsync(adminId, publicationComplaintId, isCompleted);
    }
}