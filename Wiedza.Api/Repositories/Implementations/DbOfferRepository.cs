using Wiedza.Api.Data;
using Wiedza.Core.Models.Data;

namespace Wiedza.Api.Repositories.Implementations;

public class DbOfferRepository(DataContext dataContext) : IOfferRepository
{
    public async Task<Offer> AddOfferAsync(Offer offer)
    {
        await dataContext.Offers.AddAsync(offer);
        await dataContext.SaveChangesAsync();
        return offer;
    }
}