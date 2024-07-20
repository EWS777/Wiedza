using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Core.Services;

public interface IOfferService
{
    public Task<Result<Offer>> AddOfferAsync(Guid personId, ulong publicationId, string? message);
}