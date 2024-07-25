using Wiedza.Core.Models.Data.Base;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories;

public interface IPublicationRepository
{
    Task<Result<Publication>> GetPublicationAsync(ulong publicationId);
    Task<Result<Publication>> UpdatePublicationStatusAsync(ulong publicationId, Action<Publication> update);
    Task<bool> DeletePublicationAsync(ulong publicationId);
}