using Wiedza.Core.Models;
using Wiedza.Core.Requests;
using Wiedza.Core.Responses;
using Wiedza.Core.Utilities;

namespace Wiedza.Core.Services;

public interface IProfileService
{
    public Task<Result<EditProfileResponse>> GetEditProfileAsync(Guid idPerson);
}