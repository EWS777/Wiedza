using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories;

public interface IOfferRepository
{
    Task<Result<Offer>> GetOfferAsync(Guid offerId);
    Task<Offer[]> GetReceivedOffersAsync(ulong publicationId);
    Task<Offer[]> GetSendedOffersByPersonAsync(Guid personId);
    Task<Offer> AddOfferAsync(Offer offer);
    Task<Result<Offer>> UpdateOfferStatusAsync(Guid offerId, Action<Offer> update);
    Task<bool> DeleteOfferAsync(Guid offerId);
}