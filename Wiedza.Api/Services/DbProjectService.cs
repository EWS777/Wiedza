using Wiedza.Api.Repositories;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Services;

public class DbProjectService(IProjectRepository projectRepository) : IProjectService
{
    public async Task<Project[]> GetProjectsAsync(bool onlyActive = true)
    {
        return await projectRepository.GetProjectsAsync(onlyActive);
    }

    public async Task<Project[]> GetPersonProjectsAsync(Guid personId)
    {
        return await projectRepository.GetPersonProjectsAsync(personId);
    }

    public async Task<Result<Project>> GetProjectAsync(ulong projectId)
    {
        return await projectRepository.GetProjectAsync(projectId);
    }

    public Task<Project> AddProjectAsync()
    {
        throw new NotImplementedException("Add endpoint");
    }

    public Task<Result<Project>> UpdateProject(ulong projectId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> DeleteProjectAsync(ulong projectId)
    {
        throw new NotImplementedException();
    }
}