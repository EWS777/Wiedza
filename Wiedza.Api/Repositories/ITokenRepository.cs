namespace Wiedza.Api.Repositories;

public interface ITokenRepository
{
    public Task<bool> IsRefreshTokenValidAsync(Guid userId, string session, string refresh);
    public Task SetRefreshTokenAsync(Guid userId, string session, string refresh);
}