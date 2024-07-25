using Wiedza.Api.Repositories;
using Wiedza.Core.Models.Data.Base;
using Wiedza.Core.Models.Enums;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Services;

public class DbPublicationService(
    IPublicationRepository publicationRepository) : IPublicationService
{
    public async Task<Result<Publication>> UpdatePublicationStatusAsync(ulong publicationId, PublicationStatus status,
        Guid userId)
    {
        return await publicationRepository.UpdatePublicationStatusAsync(publicationId, update =>
        {
            update.Status = status;
            update.AdministratorId = userId;
        });
    }

    public async Task<Result<bool>> DeletePublicationAsync(ulong publicationId)
    {
        var publicationResult = await publicationRepository.GetPublicationAsync(publicationId);
        if (publicationResult.IsFailed) return publicationResult.Exception;

        return await publicationRepository.DeletePublicationAsync(publicationId);
    }
}