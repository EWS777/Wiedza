using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories;

public interface IOfferRepository
{
    Task<Result<Offer>> AddOfferAsync(Offer offer);
    Task<Result<Offer>> GetOfferAsync(Guid userId, Guid offerId);
    Task<Offer[]> GetOfferListAsync(Guid userId, ulong postId);
    Task<Result<Offer>> UpdateOfferStatusAsync(Guid userId, Guid offerId, Action<Offer> update);
}