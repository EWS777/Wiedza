using System.Security.Claims;

namespace Wiedza.Api.Core.Extensions;

public static class ClaimsExtension
{
    public static Guid GetUserId(this IEnumerable<Claim> claims)
    {
        return Guid.Parse(claims.Single(p => p.Type == "userId").Value);
    }

    public static string GetRole(this IEnumerable<Claim> claims)
    {
        return claims.Single(p => p.Type == ClaimTypes.Role).Value;
    }
}