using Wiedza.Core.Models;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;
using Wiedza.Core.Utilities;

namespace Wiedza.Core.Services;

public interface IProfileService
{
    Task<Person[]> GetPersonsAsync();
    Task<Result<Profile>> GetProfileAsync(Guid personId);
    Task<Result<Profile>> GetProfileAsync(string username);
    Task<Result<Profile>> UpdateProfileAsync(Guid personId, Action<Profile> update);
    Task<Result<Person>> UpdatePersonStatusAsync(Guid personId, AccountState status, Guid adminId);
    Task<Result<bool>> DeleteProfileAsync(Guid personId, string passwordHash);
}