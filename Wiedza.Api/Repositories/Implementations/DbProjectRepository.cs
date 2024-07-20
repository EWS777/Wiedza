using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories.Implementations;

public class DbProjectRepository(DataContext dataContext) : IProjectRepository
{
    public async Task<Project[]> GetProjectsAsync(bool onlyActive = true)
    {
        var projects = dataContext.Projects
            .Include(p => p.Author)
            .Include(p => p.Category)
            .AsNoTracking();

        if (onlyActive) projects = projects.Where(p => p.Status == PublicationStatus.Active);

        return await projects.ToArrayAsync();
    }

    public async Task<Result<Project>> GetProjectAsync(ulong projectId)
    {
        var project = await dataContext.Projects.SingleOrDefaultAsync(p => p.Id == projectId);

        if (project is null) return new ProjectNotFoundException(projectId);

        return project;
    }

    public async Task<Project> AddProjectAsync(Project project)
    {
        await dataContext.Projects.AddAsync(project);
        await dataContext.SaveChangesAsync();
        return project;
    }

    public async Task<Result<Project>> UpdateProjectAsync(ulong projectId, Action<Project> updateAction)
    {
        var projectResult = await GetProjectAsync(projectId);
        if (projectResult.IsFailed) return projectResult.Exception;

        var project = projectResult.Value;

        updateAction(project);
        await dataContext.SaveChangesAsync();

        return project;
    }

    public async Task<bool> DeleteProjectAsync(ulong projectId)
    {
        var projectResult = await GetProjectAsync(projectId);
        if (projectResult.IsFailed) return false;

        var project = projectResult.Value;

        dataContext.Projects.Remove(project);
        await dataContext.SaveChangesAsync();
        return true;
    }
}