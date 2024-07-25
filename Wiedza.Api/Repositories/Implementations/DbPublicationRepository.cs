using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data.Base;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories.Implementations;

public class DbPublicationRepository(DataContext dataContext) : IPublicationRepository
{
    public async Task<Result<Publication>> GetPublicationAsync(ulong publicationId)
    {
        var publication = await dataContext.Publications.SingleOrDefaultAsync(p => p.Id == publicationId);
        if (publication is null) return new PublicationNotFoundException(publicationId);

        return publication;
    }

    public async Task<Result<Publication>> UpdatePublicationStatusAsync(ulong publicationId, Action<Publication> update)
    {
        var publicationResult = await GetPublicationAsync(publicationId);
        if (publicationResult.IsFailed) return publicationResult.Exception;

        update(publicationResult.Value);
        await dataContext.SaveChangesAsync();
        return publicationResult.Value;
    }

    public async Task<bool> DeletePublicationAsync(ulong publicationId)
    {
        var publicationResult = await GetPublicationAsync(publicationId);
        if (publicationResult.IsFailed) return false;

        dataContext.Publications.Remove(publicationResult.Value);
        await dataContext.SaveChangesAsync();
        return true;
    }
}