using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories.Implementations;

public class DbPublicationRepository(DataContext dataContext) : IPublicationRepository
{
    public async Task<Publication> AddPublicationAsync(Publication publication)
    {
        await dataContext.Publications.AddAsync(publication);
        await dataContext.SaveChangesAsync();
        return publication;
    }

    public async Task<Result<Publication>> ModifyPublicationAsync(Guid personId, Guid publicationId, Action<Publication> modify)
    {
        var publication = await dataContext.Publications.FirstOrDefaultAsync(x => x.Id == publicationId);

        if (publication is null) return new Exception("The publication is not exist!");

        if (publication.AuthorId != personId) return new Exception("The person is can not modify this publication!");

        modify(publication);
        await dataContext.SaveChangesAsync();
        return publication;
    }

    public async Task<Result<Publication>> GetPublicationAsync(Guid publicationId)
    {
        var publication = await dataContext.Publications.SingleOrDefaultAsync(x => x.Id == publicationId);
        if (publication is null) return new PublicationNotFoundException(publicationId);
        return publication;
    }
    
    public async Task<Result<List<Publication>>> GetPublicationAsync(string type)
    {
        bool isProject = type.Equals("project");
        var publication = 
            await dataContext.Publications.Where(x => x.Status == PublicationStatus.Active && x.IsProject == isProject)
                .ToListAsync();
        
        return publication;
    }

    public async Task<Result<bool>> DeletePublicationAsync(Guid publicationId)
    {
        var result = await dataContext.Publications.FirstOrDefaultAsync(x => x.Id == publicationId);
        if (result is null) return new PublicationNotFoundException(publicationId);
        result.Status = PublicationStatus.Completed;
        dataContext.Publications.Remove(result);
        await dataContext.SaveChangesAsync();
        return true;
    }
}