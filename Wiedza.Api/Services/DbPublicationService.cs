using Wiedza.Api.Core.Extensions;
using Wiedza.Api.Repositories;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;
using Wiedza.Core.Requests;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Services;

public class DbPublicationService(IPublicationRepository publicationRepository) : IPublicationService
{
    public async Task<Result<Publication>> GetPublicationAsync(ulong publicationId)
    {
        return await publicationRepository.GetPublicationAsync(publicationId);
    }

    public async Task<Publication[]> GetPublicationsAsync(bool? isProject = null)
    {
        return await publicationRepository.GetPublicationsAsync(isProject);
    }

    public async Task<Publication[]> GetPublicationsAsync(ulong fromId, int limit, bool? isProject = null)
    {
        return await publicationRepository.GetPublicationsAsync(fromId, limit, isProject);
    }

    public async Task<Publication[]> GetActivePublicationsAsync(bool? isProject = null)
    {
        return await publicationRepository.GetActivePublicationsAsync(isProject);
    }

    public async Task<Publication[]> GetActivePublicationsAsync(ulong fromId, int limit, bool? isProject = null)
    {
        return await publicationRepository.GetActivePublicationsAsync(fromId, limit, isProject);
    }

    public async Task<Publication[]> GetPersonPublications(Guid personId, bool? isProject = null)
    {
        return await publicationRepository.GetPersonPublications(personId, isProject);
    }

    public async Task<Result<Publication>> AddPublicationAsync(Guid personId, AddPublicationRequest addPublicationRequest)
    {
        return await publicationRepository.AddPublicationAsync(new Publication
        {
            IsProject = addPublicationRequest.IsProject,
            Title = addPublicationRequest.Title,
            Description = addPublicationRequest.Description,
            Price = addPublicationRequest.Price,
            CategoryId = addPublicationRequest.CategoryId,
            AuthorId = personId
        });
    }

    public async Task<Result<Publication>> UpdatePublicationAsync(Guid personId, ulong publicationId, Action<PublicationUpdateRequest> update)
    {
        var publicationResult = await publicationRepository.GetPublicationAsync(publicationId);
        if (publicationResult.IsFailed) return publicationResult.Exception;

        var publication = publicationResult.Value;
        if (publication.AuthorId != personId) return new ForbiddenException("You are not an owner of the publication!");

        var request = new PublicationUpdateRequest(publication);
        update(request);
        if (request.IsValidationFailed(out var exception)) return exception;

        return await publicationRepository.UpdatePublicationAsync(publicationId, publication1 =>
        {
            publication1.Title = request.Title;
            publication1.Description = request.Description;
            publication1.Price = request.Price;
            publication1.CategoryId = request.CategoryId;

            if (request.Status != PublicationUpdateStatus.Other)
            {
                publication1.Status = request.Status switch
                {
                    PublicationUpdateStatus.Active => PublicationStatus.Active,
                    PublicationUpdateStatus.Inactive => PublicationStatus.Inactive,
                    _ => throw new ArgumentOutOfRangeException(nameof(request.Status))
                };
            }
        });
    }

    public async Task<Result<bool>> DeletePublicationAsync(Guid personId, ulong publicationId)
    {
        var publicationResult = await publicationRepository.GetPublicationAsync(publicationId);
        if (publicationResult.IsFailed) return publicationResult.Exception;

        var publication = publicationResult.Value;

        if (publication.AuthorId != personId) return new ForbiddenException("You are not an owner of the publication!");

        return await publicationRepository.DeletePublicationAsync(publicationId);
    }
}