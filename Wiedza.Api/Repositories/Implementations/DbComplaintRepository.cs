using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Data.Base;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories.Implementations;

public class DbComplaintRepository(DataContext dataContext) : IComplaintRepository
{
    public async Task<PersonComplaint[]> GetPersonComplaintsAsync()
    {
        return await dataContext.PersonComplaints
            .Include(p => p.Person)
            .Include(p => p.Author)
            .Include(p => p.Administrator)
            .Include(p => p.AttachmentFile)
            .AsNoTracking().ToArrayAsync();
    }
    public async Task<PersonComplaint[]> GetPersonComplaintsAsync(Guid personId)
    {
        return await dataContext.PersonComplaints
            .Include(p => p.Person)
            .Include(p => p.Author)
            .Include(p => p.Administrator)
            .Include(p => p.AttachmentFile)
            .Where(p => p.PersonId == personId)
            .AsNoTracking().ToArrayAsync();
    }

    public async Task<PublicationComplaint[]> GetPublicationComplaintsAsync()
    {
        return await dataContext.PublicationComplaints
            .Include(p => p.Publication)
            .Include(p => p.Author)
            .Include(p => p.Administrator)
            .Include(p => p.AttachmentFile)
            .AsNoTracking().ToArrayAsync();
    }
    public async Task<PublicationComplaint[]> GetPublicationComplaintsAsync(ulong publicationId)
    {
        return await dataContext.PublicationComplaints
            .Include(p => p.Publication)
            .Include(p => p.Author)
            .Include(p => p.Administrator)
            .Include(p => p.AttachmentFile)
            .Where(p=>p.PublicationId == publicationId)
            .AsNoTracking().ToArrayAsync();
    }

    public async Task<Result<PersonComplaint>> GetPersonComplaintAsync(Guid complaintId)
    {
        var result = await dataContext.PersonComplaints
            .Include(p => p.Person)
            .Include(p => p.Author)
            .Include(p => p.Administrator)
            .Include(p => p.AttachmentFile)
            .SingleOrDefaultAsync(x => x.Id == complaintId);

        if (result is null) return new ComplaintNotFoundException(complaintId);
        return result;
    }
    public async Task<Result<PublicationComplaint>> GetPublicationComplaintAsync(Guid complaintId)
    {
        var result = await dataContext.PublicationComplaints
            .Include(p => p.Publication)
            .Include(p => p.Author)
            .Include(p => p.Administrator)
            .Include(p => p.AttachmentFile)
            .SingleOrDefaultAsync(x => x.Id == complaintId);
        if (result is null) return new ComplaintNotFoundException(complaintId);
        return result;
    }

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

    public async Task<Result<Complaint>> UpdateComplaintAsync(Guid complaintId, Action<Complaint> updateAction)
    {
        var singleOrDefault = await dataContext.Complaints.SingleOrDefaultAsync(p=>p.Id == complaintId);
        if (singleOrDefault is null) return new ComplaintNotFoundException(complaintId);

        updateAction(singleOrDefault);
        await dataContext.SaveChangesAsync();

        return singleOrDefault;
    }
}