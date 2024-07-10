using System.Security.Claims;
using Wiedza.Api.Core.Exceptions;

namespace Wiedza.Api.Core.Extensions;

public static class ClaimsExtension
{
    public static Guid GetUserId(this IEnumerable<Claim> claims)
    {
        var claim = claims.SingleOrDefault(p => p.Type == "userId") ?? throw new MissingClaimException("userId");
        return Guid.Parse(claim.Value);
    }

    public static string GetRole(this IEnumerable<Claim> claims)
    {
        var claim = claims.SingleOrDefault(p => p.Type == ClaimTypes.Role) ?? throw new MissingClaimException(ClaimTypes.Role);
        return claim.Value;
    }

    public static string GetRefreshToken(this IEnumerable<Claim> claims)
    {
        var claim = claims.SingleOrDefault(p => p.Type == "refresh") ?? throw new MissingClaimException("refresh");
        return claim.Value;
    }

    public static string GetSession(this IEnumerable<Claim> claims)
    {
        var claim = claims.SingleOrDefault(p => p.Type == "session") ?? throw new MissingClaimException("session");
        return claim.Value;
    }
}