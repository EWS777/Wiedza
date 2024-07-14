using Wiedza.Api.Repositories;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Services;

public class DbPublicationService(IPublicationRepository publicationRepository) : IPublicationService
{
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

    public async Task<Result<Publication>> ModifyPublicationAsync(Guid personId, Guid publicationId, Action<Publication> modify)
    {
        var findPublication = await GetPublicationAsync(publicationId);
        if (findPublication.IsFailed) return findPublication.Exception;

        var publication = findPublication.Value;
        
        modify(publication);
        
        var result = await publicationRepository.ModifyPublicationAsync(personId, publicationId, result =>
        {
            result.IsProject = publication.IsProject;
            result.Title = publication.Title;
            result.Description = publication.Description;
            result.Price = publication.Price;
            result.CategoryId = publication.CategoryId;
            result.Status = publication.Status;
        });
        return result;
    }

    public async Task<Result<Publication>> GetPublicationAsync(Guid publicationId)
    {
        var result = await publicationRepository.GetPublicationAsync(publicationId);
        if (result.IsFailed) return result.Exception;
        return result.Value;
    }
    
    public async Task<Result<List<Publication>>> GetPublicationAsync(string type)
    {
        return await publicationRepository.GetPublicationAsync(type);
    }

    public async Task<Result<bool>> DeletePublicationAsync(Guid personId, Guid publicationId)
    {
        var result = await publicationRepository.GetPublicationAsync(publicationId);
        var publication = result.Value;
        if (publication.AuthorId != personId) return new Exception("The person is can not modify this publication!");
        return await publicationRepository.DeletePublicationAsync(publicationId);
    }
}