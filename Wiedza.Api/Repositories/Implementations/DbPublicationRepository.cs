using Wiedza.Api.Data;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories.Implementations;

public class DbPublicationRepository(DataContext dataContext) : IPublicationRepository
{
    public async Task<Result<Publication>> AddPublicationAsync(Publication publication)
    {
        await dataContext.Publications.AddAsync(publication);
        await dataContext.SaveChangesAsync();
        return publication;
    }
}