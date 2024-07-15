using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories;

public interface IPublicationRepository
{
    Task<Result<Publication>> GetPublicationAsync(ulong publicationId);

    Task<Publication[]> GetPublicationsAsync(bool? isProject = null);
    Task<Publication[]> GetPublicationsAsync(ulong fromId, int limit, bool? isProject = null);

    Task<Publication[]> GetActivePublicationsAsync(bool? isProject = null);
    Task<Publication[]> GetActivePublicationsAsync(ulong fromId, int limit, bool? isProject = null);

    Task<Publication[]> GetPersonPublications(Guid personId, bool? isProject = null);

    Task<Publication> AddPublicationAsync(Publication publication);

    Task<Result<Publication>> UpdatePublicationAsync(ulong publicationId, Action<Publication> update);

    Task<Result<bool>> DeletePublicationAsync(ulong publicationId);
}