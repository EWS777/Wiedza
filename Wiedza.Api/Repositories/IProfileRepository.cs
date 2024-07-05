using Wiedza.Core.Models;
using Wiedza.Core.Requests;
using Wiedza.Core.Responses;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories;

public interface IProfileRepository
{
    public Task<Result<EditProfileResponse>> GetEditProfileAsync(Guid idPerson);
    public Task<Result<EditProfileResponse>> ChangeEditProfileAsync(EditProfileRequest editProfileRequest);
}