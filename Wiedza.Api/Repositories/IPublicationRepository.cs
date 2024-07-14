using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories;

public interface IPublicationRepository
{
    public Task<Publication> AddPublicationAsync(Publication publication);
    public Task<Result<Publication>> ModifyPublicationAsync(Guid personId, Guid publicationId, Action<Publication> modify);
    public Task<Result<Publication>> GetPublicationAsync(Guid publicationId);
    public Task<Result<List<Publication>>> GetPublicationAsync(string type);
    public Task<Result<bool>> DeletePublicationAsync(Guid publicationId);
}