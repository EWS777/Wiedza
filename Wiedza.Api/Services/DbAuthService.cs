using System.Security.Cryptography;
using Wiedza.Api.Configs;
using Wiedza.Api.Core;
using Wiedza.Api.Repositories;
using Wiedza.Core.Models;
using Wiedza.Core.Requests;
using Wiedza.Core.Responses;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Services;

public class DbAuthService(
    JwtConfiguration jwtConfiguration,
    IAuthRepository authRepository
    ) : IAuthService
{
    public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var passwordHash = GetPasswordHash(request.PasswordHash);

        var person = await authRepository.IsPersonCredentialsLegitAsync(request.UsernameOrEmail, passwordHash);

        if (person is null) return new Exception("User credentials are invalid!");

        return GenerateTokenResponse(person);
    }


    public async Task<Result<LoginResponse>> RegisterAsync(RegisterRequest request)
    {
        var passwordHash = GetPasswordHash(request.PasswordHash);

        var result = await authRepository.RegisterPersonAsync(request.Username, request.Email, passwordHash);

        if (result.IsFailed) return result.Exception;

        return GenerateTokenResponse(result.Value);
    }

    private Result<LoginResponse> GenerateTokenResponse(Person person)
    {
        var token = jwtConfiguration.GetJwtToken(person.Id, Roles.PersonRole);
        return new LoginResponse()
        {
            AuthorizationToken = token,
            UserId = person.Id,
            Role = Roles.PersonRole,
            ExpiresAt = DateTimeOffset.UtcNow.Add(jwtConfiguration.TokenLifetime).ToUnixTimeSeconds()
        };
    }

    private static string GetPasswordHash(string passwordHash)
    {
        using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(passwordHash.ToLower(), [], 10_000, HashAlgorithmName.SHA256);
        var hashBytes = rfc2898DeriveBytes.GetBytes(256);
        var result = string.Concat(hashBytes.Select(p=>p.ToString("X2")));
        return result;
    }
}