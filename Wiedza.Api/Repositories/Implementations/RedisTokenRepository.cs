using StackExchange.Redis;

namespace Wiedza.Api.Repositories.Implementations;

public class RedisTokenRepository : ITokenRepository
{
    private readonly IDatabase _database;

    public RedisTokenRepository(ConnectionMultiplexer connection)
    {
        _database = connection.GetDatabase();
    }

    public async Task<bool> IsRefreshTokenValidAsync(Guid userId, string session, string refresh)
    {
        var value = await _database.StringGetAsync($"{userId}:{session}");
        return value.HasValue && value.ToString() == refresh;
    }

    public async Task SetRefreshTokenAsync(Guid userId, string session, string refresh)
    {
        await _database.StringSetAsync($"{userId}:{session}", refresh, TimeSpan.FromDays(7));
    }
}