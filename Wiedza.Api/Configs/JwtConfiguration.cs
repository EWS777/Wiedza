using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Wiedza.Api.Core.Extensions;

namespace Wiedza.Api.Configs;

public sealed class JwtConfiguration
{
    private readonly byte[] _secret;
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public TimeSpan TokenLifetime { get; set; }

    public TokenValidationParameters TokenValidationParameters => new()
    {
        ValidIssuer = Issuer,
        ValidAudience = Audience,
        ValidateAudience = true,
        ValidateIssuer = true,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = GetSymmetricSecurityKey(),

        ValidateLifetime = true,
        LifetimeValidator = (before, expires, token, parameters) => !(DateTime.UtcNow.AddMinutes(1) > expires)
    };

    public JwtConfiguration(IConfiguration configuration)
    {
        var section = configuration.GetSectionOrThrow("Jwt");

        Issuer = section.GetValueOrThrow<string>("Issuer");
        Audience = section.GetValueOrThrow<string>("Audience");

        var tokenLifetimeSeconds = section.GetValueOrThrow<uint>("Lifetime");
        TokenLifetime = TimeSpan.FromSeconds(tokenLifetimeSeconds);

        var secretString = section.GetValueOrThrow<string>("Secret");
        _secret = Encoding.UTF8.GetBytes(secretString);
    }

    public string GetJwtToken(Guid userId, string role, string refreshToken, string session)
    {
        var token = new JwtSecurityToken(Issuer, Audience,
            [
                new Claim("userId", userId.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim("session", session),
                new Claim("refresh", refreshToken)
            ],
            expires: DateTime.UtcNow.Add(TokenLifetime),
            signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(_secret);
    }
}