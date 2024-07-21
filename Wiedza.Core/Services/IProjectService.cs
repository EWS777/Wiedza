using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Core.Services;

public interface IProjectService
{
    Task<Project[]> GetProjectsAsync(bool onlyActive = true);
    Task<Project[]> GetPersonProjectsAsync(Guid personId);
    Task<Result<Project>> GetProjectAsync(ulong projectId);
    Task<Project> AddProjectAsync();
    Task<Result<Project>> UpdateProject(ulong projectId);
    Task<Result<bool>> DeleteProjectAsync(ulong projectId);
}