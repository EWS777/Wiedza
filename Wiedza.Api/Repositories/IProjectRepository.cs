using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories;

public interface IProjectRepository
{
    Task<Project[]> GetProjectsAsync(bool onlyActive = true);
    Task<Result<Project>> GetProjectAsync(ulong projectId);
    Task<Project> AddProjectAsync(Project project);
    Task<Result<Project>> UpdateProjectAsync(ulong projectId, Action<Project> updateAction);
    Task<bool> DeleteProjectAsync(ulong projectId);
}