using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories;

public interface IComplaintRepository
{
    Task<PersonComplaint> AddPersonComplaintAsync(PersonComplaint personComplaint);
    Task<PublicationComplaint> AddPublicationComplaintAsync(PublicationComplaint publicationComplaint);
    Task<PersonComplaint[]> GetPersonComplaintsAsync();
    Task<PublicationComplaint[]> GetPublicationComplaintsAsync();
    Task<Result<PersonComplaint>> GetPersonComplaintAsync(Guid personComplaintId);
    Task<Result<PublicationComplaint>> GetPublicationComplaintAsync(Guid publicationComplaintId);
    Task<Result<PersonComplaint>> ModifyPersonComplaintAsync(Guid personComplaintId, Action<PersonComplaint> update);
    Task<Result<PublicationComplaint>> ModifyPublicationComplaintAsync(Guid publicationComplaintId, Action<PublicationComplaint> update);
}