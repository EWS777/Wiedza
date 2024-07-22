using Wiedza.Api.Repositories;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;
using Wiedza.Core.Requests;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Services;

public class DbOfferService(
    IOfferRepository offerRepository,
    IPersonRepository personRepository,
    IServiceRepository serviceRepository,
    IProjectRepository projectRepository) : IOfferService
{
    public async Task<Result<Offer>> AddOfferToServiceAsync(Guid personId, ulong publicationId, string? message)
    {
        var serviceResult = await serviceRepository.GetServiceAsync(publicationId);
        if (serviceResult.IsFailed) return new ServiceNotFoundException(publicationId);
        var service = serviceResult.Value;
        
        var personResult = await personRepository.GetPersonAsync(personId);
        var person = personResult.Value;
        
        //check if the person have enough balance to buy service
        if (person.Balance < service.Price) return new NotEnoughMoneyException("The balance is less then service's price");
        
        return await offerRepository.AddOfferAsync(new Offer
        {
            Message = message,
            PulicationId = publicationId,
            PersonId = personId,
            FreelancerProfit = service.Price*0.9f,
            CompanyProfit = service.Price - service.Price*0.9f
        });
    }

    public async Task<Result<Offer>> AddOfferToProjectAsync(Guid personId, ulong publicationId, string? message)
    {
        var projectResult = await projectRepository.GetProjectAsync(publicationId);
        if (projectResult.IsFailed) return new ProjectNotFoundException(publicationId);
        var project = projectResult.Value;
        
        return await offerRepository.AddOfferAsync(new Offer
        {
            Message = message,
            PulicationId = publicationId,
            PersonId = personId,
            FreelancerProfit = project.Price*0.9f,
            CompanyProfit = project.Price - project.Price*0.9f
        });
    }

    public async Task<Result<Offer>> GetOfferAsync(Guid userId, Guid offerId)
    {
        return await offerRepository.GetOfferAsync(userId, offerId);
    }

    public async Task<Offer[]> GetReceivedOfferListAsync(Guid userId, ulong postId)
    {
        return await offerRepository.GetReceivedOfferListAsync(userId,postId);
    }

    public async Task<Offer[]> GetSentOfferListAsync(Guid userId)
    {
        return await offerRepository.GetSentOfferListAsync(userId);
    }

    public async Task<Result<Offer>> UpdateOfferStatusAsync(Guid userId, Guid offerId, Action<UpdateOfferStatusRequest> update)
    {
        var offerResult = await offerRepository.GetOfferAsync(userId, offerId);
        if (offerResult.IsFailed) return offerResult.Exception;

        var offer = offerResult.Value;
        var request = new UpdateOfferStatusRequest(offer);
        update(request);

        return await offerRepository.UpdateOfferStatusAsync(userId, offerId, offerUpdate =>
        {
            if (request.Status != UpdateOfferStatus.Other)
            {
                offerUpdate.Status = request.Status switch
                {
                    UpdateOfferStatus.Approved => OfferStatus.Approved,
                    UpdateOfferStatus.Rejected => OfferStatus.Rejected,
                    UpdateOfferStatus.Completed => OfferStatus.Completed,
                    UpdateOfferStatus.Canceled => OfferStatus.Canceled,
                    _ => throw new ArgumentOutOfRangeException(nameof(request.Status))
                };
            }
        });
    }
}