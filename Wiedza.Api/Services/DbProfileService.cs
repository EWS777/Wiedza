﻿using Wiedza.Api.Core;
using Wiedza.Api.Core.Extensions;
using Wiedza.Api.Repositories;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Services;

public class DbProfileService(
    IPersonRepository personRepository,
    IUserSaltRepository userSaltRepository
) : IProfileService
{
    public async Task<Person[]> GetPersonsAsync()
    {
        return await personRepository.GetPersonsAsync();
    }

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

        var person = personResult.Value;
        if (person.Email == username) return new PersonNotFoundException(username);

        return new Profile(person);
    }

    public async Task<Result<Profile>> UpdateProfileAsync(Guid personId, Action<Profile> update)
    {
        var profileResult = await GetProfileAsync(personId);
        if (profileResult.IsFailed) return profileResult.Exception;

        var profile = profileResult.Value;

        profile = new Profile(profile);

        update(profile);

        if (profile.IsValidationFailed(out var exception)) return exception;

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

    public async Task<Result<Person>> UpdatePersonStatusAsync(Guid personId, AccountState status, Guid adminId)
    {
        var personResult = await personRepository.GetPersonAsync(personId);
        if (personResult.IsFailed) return personResult.Exception;

        return await personRepository.UpdatePersonStatusAsync(personId, update =>
        {
            update.AccountState = status;
            update.AdministratorId = adminId;
        });
    }

    public async Task<Result<bool>> DeleteProfileAsync(Guid personId, string passwordHash)
    {
        var personResult = await personRepository.GetPersonAsync(personId);
        if (personResult.IsFailed) return personResult.Exception;

        var person = personResult.Value;
        var salt = await userSaltRepository.GetSaltAsync(personId);

        var hash = CryptographyTools.GetPasswordHash(passwordHash, salt ?? string.Empty);

        if (person.PasswordHash != hash) return new BadRequestException("Password is incorrect!");

        return await personRepository.DeletePersonAsync(personId);
    }
}