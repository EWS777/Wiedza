using Wiedza.Core.Models.Data;

namespace Wiedza.Api.Repositories;

public interface IOfferRepository
{
    public Task<Offer> AddOfferAsync(Offer offer);
}