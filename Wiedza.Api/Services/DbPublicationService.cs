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
}