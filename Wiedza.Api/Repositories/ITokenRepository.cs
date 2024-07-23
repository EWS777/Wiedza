namespace Wiedza.Api.Repositories;

public interface ITokenRepository
{
    /// <summary>
    /// Checks refresh token for the user session
    /// </summary>
    /// <param name="userId">Person ID of the token owner</param>
    /// <param name="session">Session of the token</param>
    /// <param name="refresh">Refresh token</param>
    /// <returns>
    /// <see langword="true"/> if valid;
    /// otherwise <see langword="false"/>
    /// </returns>
    public Task<bool> IsRefreshTokenValidAsync(Guid userId, string session, string refresh);
    /// <summary>
    /// Sets refresh token for user session
    /// </summary>
    /// <param name="userId">Token owner user ID</param>
    /// <param name="session">Session for the token</param>
    /// <param name="refresh">Refresh token</param>
    /// <returns></returns>
    public Task SetRefreshTokenAsync(Guid userId, string session, string refresh);
}