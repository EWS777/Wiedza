using System.Security.Cryptography;
using Wiedza.Api.Repositories;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Services;

public class DbProfileService(IPersonRepository personRepository) : IProfileService
{
    public async Task<Result<Profile>> GetProfileAsync(Guid personId)
    {
        var personResult = await personRepository.GetPersonAsync(personId);
        if (personResult.IsFailed) return personResult.Exception;

        return new Profile(personResult.Value);
    }

    public async Task<Result<Profile>> GetProfileAsync(string username)
    {
        var personResult = await personRepository.GetPersonAsync(username);
        if (personResult.IsFailed) return personResult.Exception;

        return new Profile(personResult.Value);
    }

    public async Task<Result<Profile>> UpdateProfileAsync(Guid personId, Action<Profile> update)
    {
        var profileResult = await GetProfileAsync(personId);
        if (profileResult.IsFailed) return profileResult.Exception;

        var profile = profileResult.Value;

        update(profile);

        var usernamePerson = await personRepository.GetPersonAsync(profile.Username);

        if (usernamePerson is { IsSuccessful: true, Value: { } personValue } && personValue.Id != personId)
            return new BadRequestException($"Username `{personValue.Username}` is taken!");

        var updateResult = await personRepository.UpdatePersonAsync(personId, person =>
        {
            person.Username = profile.Username;
            person.AvatarBytes = profile.AvatarBytes;
            person.Name = profile.Name;
            person.Description = profile.Description;
        });

        return new Profile(updateResult.Value);
    }

    public async Task<Result<Person>> ChangePasswordAsync(Guid personId, ChangePasswordRequest changePasswordRequest)
    {
        var profileResult = await GetProfileAsync(personId);
        if (profileResult.IsFailed) return profileResult.Exception;
        
        var oldPasswordHash = GetPasswordHash(changePasswordRequest.oldPassword);
        var newPasswordHash = GetPasswordHash(changePasswordRequest.newPassword);

        var result = await personRepository.ChangePasswordAsync(personId, oldPasswordHash, newPasswordHash);
        return result;
    }
    
    private static string GetPasswordHash(string passwordHash)
    {
        using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(passwordHash.ToLower(), [], 10_000, HashAlgorithmName.SHA256);
        var hashBytes = rfc2898DeriveBytes.GetBytes(256);
        var result = string.Concat(hashBytes.Select(p=>p.ToString("X2")));
        return result;
    }
}