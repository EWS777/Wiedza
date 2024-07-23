using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Utilities;

namespace Wiedza.Core.Services;

public interface IComplaintService
{
    Task<PersonComplaint> AddPersonComplaintAsync(Guid authorId, Guid personId, AddComplaintRequest addComplaintRequest);
    Task<PublicationComplaint> AddPublicationComplaintAsync(Guid authorId, ulong publicationId, AddComplaintRequest addComplaintRequest);
    Task<PersonComplaint[]> GetPersonComplaintsAsync();
    Task<PublicationComplaint[]> GetPublicationComplaintsAsync();
    Task<Result<PersonComplaint>> GetPersonComplaintAsync(Guid personComplaintId);
    Task<Result<PublicationComplaint>> GetPublicationComplaintAsync(Guid publicationComplaintId);
    Task<Result<PersonComplaint>> ModifyPersonComplaintAsync(Guid adminId, Guid personComplaintId, bool isCompleted);
    Task<Result<PublicationComplaint>> ModifyPublicationComplaintAsync(Guid adminId, Guid publicationComplaintId, bool isCompleted);
}