using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Data.Base;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories;

public interface IComplaintRepository
{
    Task<PersonComplaint[]> GetPersonComplaintsAsync();
    Task<PersonComplaint[]> GetPersonComplaintsAsync(Guid personId);
    Task<PublicationComplaint[]> GetPublicationComplaintsAsync();
    Task<PublicationComplaint[]> GetPublicationComplaintsAsync(ulong publicationId);

    Task<Result<PersonComplaint>> GetPersonComplaintAsync(Guid complaintId);
    Task<Result<PublicationComplaint>> GetPublicationComplaintAsync(Guid complaintId);

    Task<PersonComplaint> AddPersonComplaintAsync(PersonComplaint personComplaint);
    Task<PublicationComplaint> AddPublicationComplaintAsync(PublicationComplaint publicationComplaint);

    Task<Result<Complaint>> UpdateComplaintAsync(Guid complaintId, Action<Complaint> updateAction);
}