using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Data.Base;
using Wiedza.Core.Requests;
using Wiedza.Core.Utilities;

namespace Wiedza.Core.Services;

public interface IComplaintService
{
    Task<PersonComplaint[]> GetPersonComplaintsAsync();
    Task<PersonComplaint[]> GetPersonComplaintsAsync(Guid personId);
    Task<PublicationComplaint[]> GetPublicationComplaintsAsync();
    Task<PublicationComplaint[]> GetPublicationComplaintsAsync(ulong publicationId);

    Task<Result<PersonComplaint>> GetPersonComplaintAsync(Guid personComplaintId);
    Task<Result<PublicationComplaint>> GetPublicationComplaintAsync(Guid publicationComplaintId);

    Task<Result<PersonComplaint>> AddPersonComplaintAsync(string username, Guid authorId, AddComplaintRequest request);

    Task<Result<PublicationComplaint>> AddPublicationComplaintAsync(ulong publicationId, Guid authorId,
        AddComplaintRequest request);

    Task<Result<Complaint>> ModifyComplaintAsync(Guid complaintId, Guid adminId, bool isCompleted);
}