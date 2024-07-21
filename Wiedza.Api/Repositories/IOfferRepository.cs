using Wiedza.Core.Models.Data;

namespace Wiedza.Api.Repositories;

public interface IOfferRepository
{
    Task<Offer> AddOfferAsync(Offer offer);
}