using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Utilities;

namespace Wiedza.Core.Services;

public interface IPublicationService
{
    public Task<Result<Publication>> AddPublicationAsync(Guid personId, AddPublicationRequest addPublicationRequest);
    public Task<Result<Publication>> ModifyPublicationAsync(Guid personId, Guid publicationId, Action<Publication> modify);
    public Task<Result<Publication>> GetPublicationAsync(Guid publicationId);
    public Task<Result<List<Publication>>> GetPublicationAsync(string type);
    public Task<Result<bool>> DeletePublicationAsync(Guid personId, Guid publicationId);
}