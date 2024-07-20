using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Core.Services;

public interface IProjectService
{
    Task<Project[]> GetProjectsAsync();
}