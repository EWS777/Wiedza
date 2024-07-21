using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Utilities;

namespace Wiedza.Core.Services;

public interface IProjectService
{
    Task<Project[]> GetProjectsAsync(bool onlyActive = true);
    Task<Project[]> GetPersonProjectsAsync(Guid personId);
    Task<Result<Project>> GetProjectAsync(ulong projectId);
    Task<Project> AddProjectAsync(Guid userId, AddPublicationRequest addPublicationRequest);
    Task<Result<Project>> UpdateProject(Guid personId, ulong projectId, Action<UpdatePublicationRequest> update);
    Task<Result<bool>> DeleteProjectAsync(Guid personId, ulong projectId);
}