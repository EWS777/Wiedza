using Wiedza.Api.Repositories;
using Wiedza.Core.Requests;
using Wiedza.Core.Responses;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Services;

public class DbProfileService(IProfileRepository profileRepository) : IProfileService
{
    public async Task<Result<EditProfileResponse>> GetEditProfileAsync(Guid idPerson)
    {
        return await profileRepository.GetEditProfileAsync(idPerson);
    }

    public async Task<Result<EditProfileResponse>> ChangeEditProfileAsync(EditProfileRequest editProfileRequest)
    {
        return await profileRepository.ChangeEditProfileAsync(editProfileRequest);
    }
}