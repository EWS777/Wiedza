using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Core.Services;

public interface IOfferService
{
    Task<Result<Offer>> GetOfferAsync(Guid userId, Guid offerId);
    Task<Result<Offer[]>> GetReceivedOffersAsync(Guid userId, ulong publicationId);
    Task<Offer[]> GetSendedOffersAsync(Guid userId);
    Task<Result<Offer>> AddOfferToPublicationAsync(Guid userId, ulong publicationId, string? message);
    Task<Result<Offer>> RespondToOfferAsync(Guid userId, Guid offerId, bool isApprove);
    Task<Result<Offer>> UpdateOfferStatusAsync(Guid userId, Guid offerId, bool isCompleted);
    Task<Result<bool>> DeleteOfferAsync(Guid userId, Guid offerId);
}