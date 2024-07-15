using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Wiedza.Api.Configs;
using Wiedza.Api.Core;
using Wiedza.Api.Core.Extensions;
using Wiedza.Api.Data;
using Wiedza.Api.Repositories;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;
using Wiedza.Core.Requests;
using Wiedza.Core.Responses;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Services;

public class DbAuthService(
    JwtConfiguration jwtConfiguration,
    IPersonRepository personRepository,
    ITokenRepository tokenRepository,
    IPersonSaltRepository personSaltRepository,
    DbUnitOfWork dbUnitOfWork
    ) : IAuthService
{
    public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var personResult = await personRepository.GetPersonAsync(request.UsernameOrEmail);

        if (personResult.IsFailed) return new InvalidCredentialsException("User credentials are invalid");

        var person = personResult.Value;
        if (person.AccountState != AccountState.Active) return new BadRequestException($"Account state is `{person.AccountState:G}`");

        var salt = await personSaltRepository.GetSaltAsync(person.Id);
        if (salt is null) throw new Exception($"Salt was null. User id `{person.Id}`");

        var passwordHash = GetPasswordHash(request.PasswordHash, salt);
        if (person.PasswordHash != passwordHash) return new InvalidCredentialsException("User credentials are invalid");

        var (session, refreshToken) = await SetUserRefreshTokenAsync(person.Id);

        return GenerateTokenResponse(person.Id, refreshToken, session);
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

            var salt = await personSaltRepository.AddPersonSalt(person.Id);
            if (salt is null) throw new Exception($"Salt was null! User id `{person.Id}`");

            var updateResult = await personRepository.UpdatePersonAsync(person.Id, person1 =>
            {
                person1.PasswordHash = GetPasswordHash(request.PasswordHash, salt);
            });

            if (updateResult.IsFailed) throw updateResult.Exception;

            await transaction.CommitAsync();

            var (session, refreshToken) = await SetUserRefreshTokenAsync(person.Id);
            return GenerateTokenResponse(person.Id, refreshToken, session);
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
        var refreshToken = validate.ClaimsIdentity.Claims.GetRefreshToken();

        var isValid = await tokenRepository.IsRefreshTokenValidAsync(userId, session, refreshToken);
        if (isValid is false) return new InvalidToken("Refresh token is invalid!");

        var refresh = await SetUserRefreshTokenAsync(userId, session);

        return GenerateTokenResponse(userId, refresh, session);
    }


    public async Task<Result<bool>> ChangePasswordAsync(Guid personId, ChangePasswordRequest changePasswordRequest)
    {
        var personResult = await personRepository.GetPersonAsync(personId);
        if (personResult.IsFailed) return personResult.Exception;

        var person = personResult.Value;

        var salt = await personSaltRepository.GetSaltAsync(personId);
        if (salt is null) throw new Exception($"Salt was null! User id `{personId}`");

        var oldPasswordHash = GetPasswordHash(changePasswordRequest.OldPasswordHash, salt);
        if (person.PasswordHash != oldPasswordHash) return new BadRequestException("Old password is incorrect");

        var newPasswordHash = GetPasswordHash(changePasswordRequest.NewPasswordHash, salt);

        var result = await personRepository.UpdatePersonAsync(personId, person1 =>
        {
            person1.PasswordHash = newPasswordHash;
        });

        if (result.IsFailed) return result.Exception;
        return true;
    }

    public async Task<Result<bool>> DeleteProfileAsync(Guid personId, string passwordHash)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Verification>> VerifyProfileAsync(Guid personId, VerifyProfileRequest verifyProfileRequest)
    {
        var personResult = await personRepository.GetPersonAsync(personId);
        if (personResult.IsFailed) return personResult.Exception;

        return await personRepository.VerifyProfileAsync(new Verification
        {
            Pesel = verifyProfileRequest.Pesel,
            Name = verifyProfileRequest.Name,
            Surname = verifyProfileRequest.Surname,
            ImageDocumentBytes = verifyProfileRequest.ImageDocumentByte,
            PersonId = personId
        });
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
        var refreshToken = GenerateRefreshToken();
        await tokenRepository.SetRefreshTokenAsync(userId, session, refreshToken);

        return refreshToken;
    }

    private LoginResponse GenerateTokenResponse(Guid userId, string refreshToken, string session, string role = Roles.PersonRole)
    {
        var token = jwtConfiguration.GetJwtToken(userId, role, refreshToken, session);
        return new LoginResponse()
        {
            AuthorizationToken = token,
            UserId = userId,
            Role = role,
            ExpiresAt = DateTimeOffset.UtcNow.Add(jwtConfiguration.TokenLifetime).ToUnixTimeSeconds()
        };
    }

    private static string GenerateRefreshToken()
    {
        Span<byte> bytes = stackalloc byte[64];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes);
    }

    private static string GetPasswordHash(string passwordHash, string salt)
    {
        using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(passwordHash.ToLower(), Encoding.UTF8.GetBytes(salt),
            10_000, HashAlgorithmName.SHA256);

        var hashBytes = rfc2898DeriveBytes.GetBytes(256);
        var result = string.Concat(hashBytes.Select(p => p.ToString("X2")));
        return result;
    }

    #endregion
}