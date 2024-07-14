using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Utilities;

namespace Wiedza.Core.Services;

public interface IPublicationService
{
    public Task<Result<Publication>> AddPublicationAsync(Guid personId, AddPublicationRequest addPublicationRequest);
}