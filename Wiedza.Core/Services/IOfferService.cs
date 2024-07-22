using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Utilities;

namespace Wiedza.Core.Services;

public interface IOfferService
{
    public Task<Result<Offer>> AddOfferToServiceAsync(Guid personId, ulong publicationId, string? message);
    public Task<Result<Offer>> AddOfferToProjectAsync(Guid personId, ulong publicationId, string? message);
    Task<Result<Offer>> GetOfferAsync(Guid userId, Guid offerId);
    Task<Offer[]> GetOfferListAsync(Guid userId, ulong postId);
    Task<Result<Offer>> UpdateOfferStatusAsync(Guid userId, Guid offerId, Action<UpdateOfferStatusRequest> update);
}