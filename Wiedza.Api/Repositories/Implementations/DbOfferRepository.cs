using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories.Implementations;

public class DbOfferRepository(DataContext dataContext) : IOfferRepository
{
    public async Task<Result<Offer>> AddOfferAsync(Offer offer)
    {
        var publication = await dataContext.Publications.SingleOrDefaultAsync(x =>
            x.Id == offer.PulicationId && x.Status == PublicationStatus.Active);
        if (publication is null) return new NotFoundException("The publication is not exist!");
            
        await dataContext.Offers.AddAsync(offer);
        await dataContext.SaveChangesAsync();
        return offer;
    }

    public async Task<Result<Offer>> GetOfferAsync(Guid userId, Guid offerId)
    {
        var offer = await dataContext.Offers.SingleOrDefaultAsync(x => x.Id == offerId);
        if (offer is null) return new OfferNotFoundException(offerId); 
        
        offer = await dataContext.Offers.SingleOrDefaultAsync(x=>x.Id == offerId && (x.PersonId == userId || x.Publication!.AuthorId == userId));
        if (offer is null) return new ForbiddenException("You don't have permissions to this offer!");

        return offer;
    }

    public async Task<Offer[]> GetReceivedOfferListAsync(Guid userId, ulong postId)
    {
        return await dataContext.Offers
            .Include(x=>x.Publication!.Author)
            .AsNoTracking()
            .Where(x => x.Publication != null && x.Publication.Id == postId && x.Publication.AuthorId == userId
            && x.Status == OfferStatus.New).ToArrayAsync();
    }

    public async Task<Offer[]> GetSentOfferListAsync(Guid userId)
    {
        return await dataContext.Offers
            .AsNoTracking()
            .Where(x => x.PersonId == userId).ToArrayAsync();
    }

    public async Task<Result<Offer>> UpdateOfferStatusAsync(Guid userId, Guid offerId, Action<Offer> update)
    {
        var offerResult = await dataContext.Offers.SingleOrDefaultAsync(x=>x.Id == offerId && x.Publication!.AuthorId == userId);
        if (offerResult is null) return new ForbiddenException("You don't have permissions to modify status!");
        update(offerResult);
        if (offerResult.Status == OfferStatus.Completed)
        {
            var publication =
                await dataContext.Publications.SingleOrDefaultAsync(x => x.Id == offerResult.PulicationId);
            if (publication is null) return new NotFoundException("The publication is not found!");
            publication.Status = PublicationStatus.Completed;
        }
        await dataContext.SaveChangesAsync();
        return offerResult;
    }
}