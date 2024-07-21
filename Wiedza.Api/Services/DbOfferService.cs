using Wiedza.Api.Repositories;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Services;

public class DbOfferService(
    IOfferRepository offerRepository,
    IPersonRepository personRepository) : IOfferService
{
    public async Task<Result<Offer>> AddOfferAsync(Guid personId, ulong publicationId, string? message)
    {
        throw new NotImplementedException();
        //var publicationResult = await publicationRepository.GetPublicationAsync(publicationId);
        //if (publicationResult.IsFailed) return new PublicationNotFoundException(publicationId);
        //var publication = publicationResult.Value;
        
        //var personResult = await personRepository.GetPersonAsync(personId);
        //var person = personResult.Value;
        
        
        ////check if the person have enough balance to buy service
        //if (publication.IsProject is false && person.Balance < publication.Price)
        //    return new NotEnoughMoneyException("The balance is less then service's price");
        
        //return await offerRepository.AddOfferAsync(new Offer
        //{
        //    Message = message,
        //    PulicationId = publicationId,
        //    PersonId = personId,
        //    FreelancerProfit = publication.Price*0.9f,
        //    CompanyProfit = publication.Price - publication.Price*0.9f
        //});
    }
}