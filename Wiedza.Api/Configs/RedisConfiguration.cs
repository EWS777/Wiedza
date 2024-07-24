using StackExchange.Redis;
using Wiedza.Api.Core.Extensions;

namespace Wiedza.Api.Configs;

public sealed class RedisConfiguration
{
    public EndPointCollection EndPoints { get; } = new();
    public ConfigurationOptions ConfigurationOptions => new()
    {
        EndPoints = EndPoints
    };

    public RedisConfiguration(IConfiguration configuration)
    {
        var section = configuration.GetSectionOrThrow("Redis");

        var endPoints = section.GetValueOrThrow<string[]>("EndPoints");
        foreach (var endPoint in endPoints) EndPoints.Add(endPoint);
    }
}