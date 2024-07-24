using System.Reflection;

namespace Wiedza.Api.Core.Extensions;

public static class EnumExtensions
{
    public static TAttribute? GetEnumAttribute<TEnum, TAttribute>(this TEnum @enum)
    where TEnum : Enum
    where TAttribute : Attribute
    {
        var type = @enum.GetType();
        var member = type.GetMember(@enum.ToString());
        return member[0].GetCustomAttribute<TAttribute>();
    }
}