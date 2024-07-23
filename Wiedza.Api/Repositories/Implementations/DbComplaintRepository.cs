using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories.Implementations;

public class DbComplaintRepository(DataContext dataContext) : IComplaintRepository
{
    public async Task<PersonComplaint> AddPersonComplaintAsync(PersonComplaint personComplaint)
    {
        await dataContext.PersonComplaints.AddAsync(personComplaint);
        await dataContext.SaveChangesAsync();
        return personComplaint;
    }

    public async Task<PublicationComplaint> AddPublicationComplaintAsync(PublicationComplaint publicationComplaint)
    {
        await dataContext.PublicationComplaints.AddAsync(publicationComplaint);
        await dataContext.SaveChangesAsync();
        return publicationComplaint;
    }

    public async Task<PersonComplaint[]> GetPersonComplaintsAsync()
    {
        return await dataContext.PersonComplaints.ToArrayAsync();
    }

    public async Task<PublicationComplaint[]> GetPublicationComplaintsAsync()
    {
        return await dataContext.PublicationComplaints.ToArrayAsync();
    }

    public async Task<Result<PersonComplaint>> GetPersonComplaintAsync(Guid personComplaintId)
    {
        var result = await dataContext.PersonComplaints.SingleOrDefaultAsync(x => x.Id == personComplaintId);
        if (result is null) return new ComplaintNotFoundException(personComplaintId);
        return result;
    }

    public async Task<Result<PublicationComplaint>> GetPublicationComplaintAsync(Guid publicationComplaintId)
    {
        var result = await dataContext.PublicationComplaints.SingleOrDefaultAsync(x => x.Id == publicationComplaintId);
        if (result is null) return new ComplaintNotFoundException(publicationComplaintId);
        return result;
    }

    public async Task<Result<PersonComplaint>> ModifyPersonComplaintAsync(Guid personComplaintId, Action<PersonComplaint> update)
    {
        var complaintResult = await GetPersonComplaintAsync(personComplaintId);
        if (complaintResult.IsFailed) return complaintResult.Exception;

        var complaint = complaintResult.Value;
        update(complaint);
        await dataContext.SaveChangesAsync();
        return complaint;
    }

    public async Task<Result<PublicationComplaint>> ModifyPublicationComplaintAsync(Guid publicationComplaintId, Action<PublicationComplaint> update)
    {
        var complaintResult = await GetPublicationComplaintAsync(publicationComplaintId);
        if (complaintResult.IsFailed) return complaintResult.Exception;

        var complaint = complaintResult.Value;
        update(complaint);
        await dataContext.SaveChangesAsync();
        return complaint;
    }
}