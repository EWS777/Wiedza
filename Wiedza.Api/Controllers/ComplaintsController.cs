using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wiedza.Api.Core;
using Wiedza.Api.Core.Extensions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Data.Base;
using Wiedza.Core.Requests;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Controllers;

[ApiController, Route("[controller]")]
public class ComplaintsController(
    IComplaintService complaintService
    ) : ControllerBase
{
    [HttpGet, Route("persons/*"), Authorize(Policy = Policies.AdminPolicy)]
    public async Task<ActionResult<PersonComplaint[]>> GetPersonComplaints()
    {
        return await complaintService.GetPersonComplaintsAsync();
    }

    [HttpGet, Route("persons/{personId:guid}")]
    public async Task<ActionResult<PersonComplaint[]>> GetPersonComplaints(Guid personId)
    {
        return await complaintService.GetPersonComplaintsAsync(personId);
    }

    [HttpGet, Route("publications/*"), Authorize(Policy = Policies.AdminPolicy)]
    public async Task<ActionResult<PublicationComplaint[]>> GetPublicationComplaints()
    {
        return await complaintService.GetPublicationComplaintsAsync();
    }

    [HttpGet, Route("publications/{publicationId}")]
    public async Task<ActionResult<PublicationComplaint[]>> GetPublicationComplaints(ulong publicationId)
    {
        return await complaintService.GetPublicationComplaintsAsync(publicationId);
    }

    [HttpGet, Route("persons"), Authorize(Policy = Policies.AdminPolicy)]
    public async Task<Result<PersonComplaint>> GetPersonComplaint([FromQuery(Name = "id")] Guid personComplaintId)
    {
        return await complaintService.GetPersonComplaintAsync(personComplaintId);
    }

    [HttpGet, Route("publications"), Authorize(Policy = Policies.AdminPolicy)]
    public async Task<Result<PublicationComplaint>> GetPublicationComplaint([FromQuery(Name = "id")] Guid publicationComplaintId)
    {
        return await complaintService.GetPublicationComplaintAsync(publicationComplaintId);
    }

    [HttpPost, Route("persons/{username}/add"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<ActionResult<PersonComplaint>> AddPersonComplaint(string username, AddComplaintRequest request)
    {
        var authorId = User.Claims.GetUserId();
        var result = await complaintService.AddPersonComplaintAsync(username, authorId, request);
        return result.Match(complaint => complaint, e => throw e);
    }

    [HttpPost, Route("publications/{publicationId}/add"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<ActionResult<PublicationComplaint>> AddPublicationComplaint(ulong publicationId, AddComplaintRequest request)
    {
        var authorId = User.Claims.GetUserId();
        var result = await complaintService.AddPublicationComplaintAsync(publicationId, authorId, request);
        return result.Match(complaint => complaint, e => throw e);
    }

    [HttpPost, Route("{complaintId:guid}"), Authorize(Policy = Policies.AdminPolicy)]
    public async Task<ActionResult<Complaint>> ModifyComplaint(Guid complaintId, [FromQuery] bool isCompleted)
    {
        var adminId = User.Claims.GetUserId();
        var result = await complaintService.ModifyComplaintAsync(complaintId, adminId, isCompleted);
        return result.Match(complaint => complaint, e => throw e);
    }
}