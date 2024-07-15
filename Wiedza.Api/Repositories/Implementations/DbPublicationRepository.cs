using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories.Implementations;

public class DbPublicationRepository(DataContext dataContext) : IPublicationRepository
{
    public async Task<Result<Publication>> GetPublicationAsync(ulong publicationId)
    {
        var publication = await dataContext.Publications.SingleOrDefaultAsync(x => x.Id == publicationId);
        if (publication is null) return new PublicationNotFoundException(publicationId);
        return publication;
    }

    public async Task<Publication[]> GetPublicationsAsync(bool? isProject = null)
    {
        var publications = dataContext.Publications;

        if (isProject is not null)
        {
            return await publications.Where(p => p.IsProject == isProject).AsNoTracking().ToArrayAsync();
        }

        return await publications.AsNoTracking().ToArrayAsync();
    }

    public async Task<Publication[]> GetActivePublicationsAsync(bool? isProject = null)
    {
        var publications = dataContext.Publications.Where(p => p.Status == PublicationStatus.Active);

        if (isProject is not null)
        {
            return await publications.Where(p => p.IsProject == isProject).AsNoTracking().ToArrayAsync();
        }

        return await publications.AsNoTracking().ToArrayAsync();
    }

    public async Task<Publication[]> GetPublicationsAsync(ulong fromId, int limit, bool? isProject = null)
    {
        var publications = dataContext.Publications
            .OrderByDescending(p=>p.Id)
            .Where(p => p.Id < fromId);

        if (isProject is not null) publications = publications.Where(p => p.IsProject == isProject);

        return await publications
            .Take(limit)
            .AsNoTracking().ToArrayAsync();
    }

    public async Task<Publication[]> GetActivePublicationsAsync(ulong fromId, int limit, bool? isProject = null)
    {
        var publications = dataContext.Publications
            .OrderByDescending(p => p.Id)
            .Where(p => p.Id < fromId)
            .Where(p => p.Status == PublicationStatus.Active);

        if (isProject is not null) publications = publications.Where(p => p.IsProject == isProject);

        return await publications
            .Take(limit)
            .AsNoTracking().ToArrayAsync();
    }

    public async Task<Publication[]> GetPersonPublications(Guid personId, bool? isProject = null)
    {
        var publications = dataContext.Publications
            .Where(p => p.AuthorId == personId);

        if (isProject is not null) publications = publications.Where(p => p.IsProject == isProject);

        return await publications.AsNoTracking().ToArrayAsync();
    }

    public async Task<Publication> AddPublicationAsync(Publication publication)
    {
        await dataContext.Publications.AddAsync(publication);
        await dataContext.SaveChangesAsync();
        return publication;
    }

    public async Task<Result<Publication>> UpdatePublicationAsync(ulong publicationId, Action<Publication> update)
    {
        var publication = await dataContext.Publications.FirstOrDefaultAsync(x => x.Id == publicationId);

        if (publication is null) return new PublicationNotFoundException(publicationId);
        
        update(publication);

        await dataContext.SaveChangesAsync();
        return publication;
    }

    public async Task<Result<bool>> DeletePublicationAsync(ulong publicationId)
    {
        var publicationResult = await GetPublicationAsync(publicationId);

        if (publicationResult.IsFailed) return publicationResult.Exception;

        var publication = publicationResult.Value;

        dataContext.Publications.Remove(publication);

        await dataContext.SaveChangesAsync();
        return true;
    }
}