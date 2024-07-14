using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories;

public interface IPublicationRepository
{
    public Task<Result<Publication>> AddPublicationAsync(Publication publication);
}