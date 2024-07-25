using Wiedza.Core.Models.Data.Base;
using Wiedza.Core.Models.Enums;
using Wiedza.Core.Utilities;

namespace Wiedza.Core.Services;

public interface IPublicationService
{
    Task<Result<Publication>> UpdatePublicationStatusAsync(ulong publicationId, PublicationStatus status, Guid userId);
    Task<Result<bool>> DeletePublicationAsync(ulong publicationId);
}