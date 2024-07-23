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
}