using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Utilities;

namespace Wiedza.Core.Services;

public interface IPublicationService
{
    public Task<Result<Publication>> GetPublicationAsync(ulong publicationId);

    Task<Publication[]> GetPublicationsAsync(bool? isProject = null);
    Task<Publication[]> GetPublicationsAsync(ulong fromId, int limit, bool? isProject = null);

    Task<Publication[]> GetActivePublicationsAsync(bool? isProject = null);
    Task<Publication[]> GetActivePublicationsAsync(ulong fromId, int limit, bool? isProject = null);

    Task<Publication[]> GetPersonPublications(Guid personId, bool? isProject = null);

    public Task<Result<Publication>> AddPublicationAsync(Guid personId, AddPublicationRequest addPublicationRequest);

    public Task<Result<Publication>> UpdatePublicationAsync(Guid personId, ulong publicationId, Action<PublicationUpdateRequest> update);

    public Task<Result<bool>> DeletePublicationAsync(Guid personId, ulong publicationId);
}