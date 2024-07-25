using System.IdentityModel.Tokens.Jwt;
using Wiedza.Api.Configs;
using Wiedza.Api.Core;
using Wiedza.Api.Core.Extensions;
using Wiedza.Api.Data;
using Wiedza.Api.Repositories;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Data.Base;
using Wiedza.Core.Models.Enums;
using Wiedza.Core.Requests;
using Wiedza.Core.Responses;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Services;

public class DbAuthService(
    JwtConfiguration jwtConfiguration,
    IUserRepository userRepository,
    IPersonRepository personRepository,
    ITokenRepository tokenRepository,
    IUserSaltRepository userSaltRepository,
    DbUnitOfWork dbUnitOfWork
) : IAuthService
{
    public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var userResult = await userRepository.GetUserAsync(request.UsernameOrEmail);

        if (userResult.IsFailed) return new InvalidCredentialsException("User credentials are invalid");

        var user = userResult.Value;
        if (user.AccountState != AccountState.Active)
            return new BadRequestException($"Account state is `{user.AccountState:G}`");

        var salt = await userSaltRepository.GetSaltAsync(user.Id);
        if (salt is null) throw new Exception($"Salt was null. User id `{user.Id}`");

        var passwordHash = CryptographyTools.GetPasswordHash(request.PasswordHash, salt);
        if (user.PasswordHash != passwordHash) return new InvalidCredentialsException("User credentials are invalid");

        var (session, refreshToken) = await SetUserRefreshTokenAsync(user.Id);

        var role = user.UserType switch
        {
            UserType.Administrator => Roles.AdministratorRole,
            UserType.Person => Roles.PersonRole,
            _ => throw new ArgumentOutOfRangeException(nameof(user.UserType))
        };

        return GenerateTokenResponse(user.Id, refreshToken, session, role);
    }


    public async Task<Result<LoginResponse>> RegisterAsync(RegisterRequest request)
    {
        await using var transaction = await dbUnitOfWork.BeginTransactionAsync();

        try
        {
            var personResult = await personRepository.AddPersonAsync(new Person
            {
                Email = request.Email,
                Username = request.Username,
                PasswordHash = request.PasswordHash
            });
            if (personResult.IsFailed) throw personResult.Exception;

            var person = personResult.Value;

            var salt = await userSaltRepository.AddPersonSalt(person.Id);
            if (salt is null) throw new Exception($"Salt was null! Person id `{person.Id}`");

            var updateResult = await personRepository.UpdatePersonAsync(person.Id,
                person1 => { person1.PasswordHash = CryptographyTools.GetPasswordHash(request.PasswordHash, salt); });

            if (updateResult.IsFailed) throw updateResult.Exception;

            await transaction.CommitAsync();

            var (session, refreshToken) = await SetUserRefreshTokenAsync(person.Id);
            return GenerateTokenResponse(person.Id, refreshToken, session, Roles.PersonRole);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return e;
        }
    }

    public async Task<Result<LoginResponse>> RefreshTokenAsync(string jwtToken)
    {
        var parameters = jwtConfiguration.TokenValidationParameters;
        parameters.LifetimeValidator = null;
        parameters.ValidateLifetime = false;

        var validate = await new JwtSecurityTokenHandler().ValidateTokenAsync(jwtToken, parameters);
        if (validate.IsValid is false) return new InvalidToken("Token is invalid!");

        var userId = validate.ClaimsIdentity.Claims.GetUserId();
        var session = validate.ClaimsIdentity.Claims.GetSession();
        var role = validate.ClaimsIdentity.Claims.GetRole();
        var refreshToken = validate.ClaimsIdentity.Claims.GetRefreshToken();

        var isValid = await tokenRepository.IsRefreshTokenValidAsync(userId, session, refreshToken);
        if (isValid is false) return new InvalidToken("Refresh token is invalid!");

        var refresh = await SetUserRefreshTokenAsync(userId, session);

        return GenerateTokenResponse(userId, refresh, session, role);
    }


    public async Task<Result<bool>> ChangePasswordAsync(Guid personId, ChangePasswordRequest changePasswordRequest)
    {
        var personResult = await personRepository.GetPersonAsync(personId);
        if (personResult.IsFailed) return personResult.Exception;

        var person = personResult.Value;

        var salt = await userSaltRepository.GetSaltAsync(personId);
        if (salt is null) throw new Exception($"Salt was null! Person id `{personId}`");

        var oldPasswordHash = CryptographyTools.GetPasswordHash(changePasswordRequest.OldPasswordHash, salt);
        if (person.PasswordHash != oldPasswordHash) return new BadRequestException("Old password is incorrect");

        var newPasswordHash = CryptographyTools.GetPasswordHash(changePasswordRequest.NewPasswordHash, salt);

        var result =
            await personRepository.UpdatePersonAsync(personId, person1 => { person1.PasswordHash = newPasswordHash; });

        if (result.IsFailed) return result.Exception;
        return true;
    }
    
    #region Private

    private async Task<(string session, string refreshToken)> SetUserRefreshTokenAsync(Guid userId)
    {
        var session = Guid.NewGuid().ToString();
        var refreshToken = await SetUserRefreshTokenAsync(userId, session);

        return (session, refreshToken);
    }

    private async Task<string> SetUserRefreshTokenAsync(Guid userId, string session)
    {
        var refreshToken = CryptographyTools.GenerateToken();
        await tokenRepository.SetRefreshTokenAsync(userId, session, refreshToken);

        return refreshToken;
    }

    private LoginResponse GenerateTokenResponse(Guid userId, string refreshToken, string session, string role)
    {
        var token = jwtConfiguration.GetJwtToken(userId, role, refreshToken, session);
        return new LoginResponse
        {
            AuthorizationToken = token,
            PersonId = userId,
            Role = role,
            ExpiresAt = DateTimeOffset.UtcNow.Add(jwtConfiguration.TokenLifetime).ToUnixTimeSeconds()
        };
    }

    #endregion
}