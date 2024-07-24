using Wiedza.Api.Core.Extensions;
using Wiedza.Api.Repositories;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;
using Wiedza.Core.Requests;
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

    public async Task<Project> AddProjectAsync(Guid userId, AddPublicationRequest addPublicationRequest)
    {
        return await projectRepository.AddProjectAsync(new Project
        {
            Title = addPublicationRequest.Title,
            Description = addPublicationRequest.Description,
            Price = addPublicationRequest.Price,
            CategoryId = addPublicationRequest.CategoryId,
            AuthorId = userId
        });
    }

    public async Task<Result<Project>> UpdateProject(Guid personId, ulong projectId,
        Action<UpdatePublicationRequest> update)
    {
        var projectResult = await projectRepository.GetProjectAsync(projectId);
        if (projectResult.IsFailed) return projectResult.Exception;

        var project = projectResult.Value;
        if (project.AuthorId != personId) return new ForbiddenException("You are not an owner of this project!");

        var request = new UpdatePublicationRequest(project);
        update(request);

        if (request.IsValidationFailed(out var exception)) return exception;

        return await projectRepository.UpdateProjectAsync(projectId, projectUpdate =>
        {
            projectUpdate.Title = request.Title;
            projectUpdate.Description = request.Description;
            projectUpdate.Price = request.Price;
            projectUpdate.CategoryId = request.CategoryId;

            if (request.Status != PublicationUpdateStatus.Other)
                projectUpdate.Status = request.Status switch
                {
                    PublicationUpdateStatus.Active => PublicationStatus.Active,
                    PublicationUpdateStatus.Inactive => PublicationStatus.Inactive,
                    _ => throw new ArgumentOutOfRangeException(nameof(request.Status))
                };
        });
    }

    public async Task<Result<bool>> DeleteProjectAsync(Guid personId, ulong projectId)
    {
        var projectResult = await projectRepository.GetProjectAsync(projectId);
        if (projectResult.IsFailed) return projectResult.Exception;

        var project = projectResult.Value;

        if (project.AuthorId != personId) return new ForbiddenException("You are not an owner of this project!");

        return await projectRepository.DeleteProjectAsync(projectId);
    }
}