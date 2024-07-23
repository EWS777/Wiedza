﻿using Wiedza.Api.Repositories;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Services;

public class DbOfferService(
    IStatisticRepository statisticRepository,
    IOfferRepository offerRepository,
    IPublicationRepository publicationRepository,
    IPersonRepository personRepository) : IOfferService
{
    public async Task<Result<Offer>> GetOfferAsync(Guid userId, Guid offerId)
    {
        var offerResult = await offerRepository.GetOfferAsync(offerId);
        if (offerResult.IsFailed) return offerResult.Exception;

        var offer = offerResult.Value;

        if (offer.PersonId != userId && offer.Publication?.AuthorId != userId)
            return new ForbiddenException("You don't have permission to get this offer!");

        return offer;
    }

    public async Task<Result<Offer[]>> GetReceivedOffersAsync(Guid userId, ulong publicationId)
    {
        var publicationResult = await publicationRepository.GetPublicationAsync(publicationId);
        if (publicationResult.IsFailed) return publicationResult.Exception;

        var publication = publicationResult.Value;

        if (publication.AuthorId != userId) return new ForbiddenException("You are not owner of publication!");

        return await offerRepository.GetReceivedOffersAsync(publicationId);
    }

    public async Task<Offer[]> GetSendedOffersAsync(Guid userId)
    {
        return await offerRepository.GetSendedOffersByPersonAsync(userId);
    }

    public async Task<Result<Offer>> AddOfferToPublicationAsync(Guid userId, ulong publicationId, string? message)
    {
        var publicationResult = await publicationRepository.GetPublicationAsync(publicationId);
        if (publicationResult.IsFailed) return publicationResult.Exception;

        var publication = publicationResult.Value;

        if (publication.Status != PublicationStatus.Active) return new BadRequestException("Publication is not active!");

        if (publication.AuthorId == userId)
            return new BadRequestException("You cannot send offer on your own publication!");

        if (publication.PublicationType == typeof(Service))
        {
            var personResult = await personRepository.GetPersonAsync(userId);
            if (personResult.IsFailed) return personResult.Exception;
            var person = personResult.Value;

            if (person.Balance < publication.Price)
                return new NotEnoughMoneyException($"Price of service is {publication.Price}");
        }

        return await offerRepository.AddOfferAsync(new Offer
        {
            PersonId = userId,
            Message = message,
            PulicationId = publicationId
        });
    }

    public async Task<Result<bool>> DeleteOfferAsync(Guid userId, Guid offerId)
    {
        var offerResult = await offerRepository.GetOfferAsync(offerId);
        if (offerResult.IsFailed) return offerResult.Exception;

        var offer = offerResult.Value;

        if (offer.PersonId != userId)
            return new ForbiddenException("You are not owner of the offer!");

        if (offer.Status is OfferStatus.Approved)
            return new BadRequestException("Offer is approved. You cannot delete this offer!");

        return await offerRepository.DeleteOfferAsync(offerId);
    }

    public async Task<Result<Offer>> RespondToOfferAsync(Guid userId, Guid offerId, bool isApprove)
    {
        var offerResult = await offerRepository.GetOfferAsync(offerId);
        if (offerResult.IsFailed) throw offerResult.Exception;

        var offer = offerResult.Value;

        if (offer.Publication?.AuthorId != userId)
            return new ForbiddenException("You don't have permission to change this offer! You are not a owner of the publication!");

        if (offer.Status != OfferStatus.New)
            return new BadRequestException("Offer's status has been already set!");

        return await offerRepository.UpdateOfferStatusAsync(offerId, offerUpdate =>
        {
            offerUpdate.Status = isApprove ? OfferStatus.Approved : OfferStatus.Rejected;
        });
    }

    public async Task<Result<Offer>> ChangeOfferStatusAsync(Guid userId, Guid offerId, bool isCompleted)
    {
        var offerResult = await offerRepository.GetOfferAsync(offerId);
        if (offerResult.IsFailed) return offerResult.Exception;
        
        var offer = offerResult.Value;

        if (offer.Publication?.AuthorId != userId)
            return new ForbiddenException("You don't have permission to change this offer! You are not a owner of the publication!");

        if (offer.Status != OfferStatus.Approved)
            return new BadRequestException("Offer's status must be `Approved`!");

        if (isCompleted)
        {
            if (offer.Publication.PublicationType == typeof(Service))
            {
                offer.Person!.Balance -= offer.Publication.Price;
                offer.Publication.Author.Balance += offer.CompanyProfit;
                await statisticRepository.AddIncomeBalanceAsync(offer.CompanyProfit);
            }
            else if (offer.Publication.PublicationType == typeof(Project))
            {
                offer.Person!.Balance += offer.Publication.Price;
                offer.Publication.Author.Balance -= offer.CompanyProfit;
                await statisticRepository.AddIncomeBalanceAsync(offer.CompanyProfit);
                
                offer.Publication.Status = PublicationStatus.Completed;
            }
            else return new BadRequestException($"Unknown type of publication {offer.Publication.PublicationType}");

        }

        return await offerRepository.UpdateOfferStatusAsync(offerId, (offerUpdate =>
        {
            offerUpdate.Status = isCompleted ? OfferStatus.Completed : OfferStatus.Canceled;
        }));
    }
}