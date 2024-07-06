using Wiedza.Core.Models;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Utilities;

namespace Wiedza.Core.Services;

public interface IProfileService
{
    public Task<Result<Profile>> GetProfileAsync(Guid personId);
    public Task<Result<Profile>> GetProfileAsync(string username);
    public Task<Result<Profile>> UpdateProfileAsync(Guid personId, Action<Profile> update);
    public Task<Result<Person>> ChangePasswordAsync(Guid personId, ChangePasswordRequest changePasswordRequest);
}