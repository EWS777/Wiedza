using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories.Implementations;

public class DbOfferRepository(DataContext dataContext) : IOfferRepository
{
    public async Task<Result<Offer>> GetOfferAsync(Guid offerId)
    {
        var offer = await dataContext.Offers
            .Include(p => p.Person)
            .Include(p => p.Publication)
            .ThenInclude(p => p!.Author)
            .SingleOrDefaultAsync(p => p.Id == offerId);
        if (offer is null) return new OfferNotFoundException(offerId);

        return offer;
    }

    public async Task<Offer[]> GetReceivedOffersAsync(ulong publicationId)
    {
        return await dataContext.Offers
            .Include(x => x.Publication)
            .ThenInclude(p => p!.Author)
            .Where(p => p.PulicationId == publicationId)
            .AsNoTracking().ToArrayAsync();
    }

    public async Task<Offer[]> GetSendedOffersByPersonAsync(Guid personId)
    {
        return await dataContext.Offers
            .Include(p => p.Person)
            .Include(p => p.Publication)
            .ThenInclude(p => p!.Author)
            .Where(x => x.PersonId == personId)
            .AsNoTracking().ToArrayAsync();
    }

    public async Task<Offer> AddOfferAsync(Offer offer)
    {
        await dataContext.Offers.AddAsync(offer);
        await dataContext.SaveChangesAsync();
        return offer;
    }

    public async Task<Result<Offer>> UpdateOfferStatusAsync(Guid offerId, Action<Offer> update)
    {
        var offerResult = await GetOfferAsync(offerId);
        if (offerResult.IsFailed) return offerResult.Exception;

        var offer = offerResult.Value;
        update(offer);
        await dataContext.SaveChangesAsync();
        return offer;
    }

    public async Task<bool> DeleteOfferAsync(Guid offerId)
    {
        var offerResult = await GetOfferAsync(offerId);
        if (offerResult.IsFailed) return false;

        var offer = offerResult.Value;

        dataContext.Offers.Remove(offer);
        await dataContext.SaveChangesAsync();
        return true;
    }
}