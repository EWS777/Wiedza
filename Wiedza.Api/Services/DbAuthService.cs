using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Wiedza.Api.Configs;
using Wiedza.Api.Core;
using Wiedza.Api.Core.Extensions;
using Wiedza.Api.Repositories;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Requests;
using Wiedza.Core.Responses;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Services;

public class DbAuthService(
    JwtConfiguration jwtConfiguration,
    IAuthRepository authRepository,
    IPersonRepository personRepository,
    ITokenRepository tokenRepository
    ) : IAuthService
{
    public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var passwordHash = GetPasswordHash(request.PasswordHash);

        var person = await authRepository.IsPersonCredentialsLegitAsync(request.UsernameOrEmail, passwordHash);

        if (person is null) return new InvalidCredentialsException("User credentials are invalid!");

        var (session, refreshToken) = await SetUserRefreshTokenAsync(person.Id);

        return GenerateTokenResponse(person.Id, refreshToken, session);
    }


    public async Task<Result<LoginResponse>> RegisterAsync(RegisterRequest request)
    {
        var passwordHash = GetPasswordHash(request.PasswordHash);

        var result = await authRepository.RegisterPersonAsync(request.Username, request.Email, passwordHash);

        if (result.IsFailed) return result.Exception;

        var person = result.Value;

        var (session, refreshToken) = await SetUserRefreshTokenAsync(person.Id);

        return GenerateTokenResponse(person.Id, refreshToken, session);
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

        var oldPasswordHash = GetPasswordHash(changePasswordRequest.OldPasswordHash);
        if (person.PasswordHash != oldPasswordHash) return new BadRequestException("Old password is incorrect");

        var newPasswordHash = GetPasswordHash(changePasswordRequest.NewPasswordHash);

        var result = await personRepository.UpdatePersonAsync(personId, person1 =>
        {
            person1.PasswordHash = newPasswordHash;
        });

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

    private static string GetPasswordHash(string passwordHash)
    {
        using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(passwordHash.ToLower(), [], 10_000, HashAlgorithmName.SHA256);
        var hashBytes = rfc2898DeriveBytes.GetBytes(256);
        var result = string.Concat(hashBytes.Select(p => p.ToString("X2")));
        return result;
    }

    #endregion
}