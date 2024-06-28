using Wiedza.Api.Core.Exceptions;

namespace Wiedza.Api.Core.Extensions;

internal static class ConfigurationExtensions
{
    public static IConfigurationSection GetSectionOrThrow(this IConfiguration configuration, string key)
    {
        var section = configuration.GetSection(key);
        if (section.Exists() is false)
            throw new ConfigurationNoSectionException($"{section.Path} is not found in configuration!");
        return section;
    }

    public static TValue GetValueOrThrow<TValue>(this IConfigurationSection section)
    {
        var value = section.Get<TValue?>();
        if (value is null)
            throw new ConfigurationNoValueException(
                $"{section.Path} doesn't contains the value or value is invalid. Required type is {typeof(TValue)}");

        return value;
    }
}