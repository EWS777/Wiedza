using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Wiedza.Api.Data.ValueConvertors;

public class TypeValueConverter<TEnum> : ValueConverter<TEnum, string> where TEnum : Enum
{
    public TypeValueConverter() : base(@enum => ToProvider(@enum), name => ToModel(name))
    {
    }

    private static TEnum ToModel(string name)
    {
        return (TEnum)Enum.Parse(typeof(TEnum), name);
    }

    private static string ToProvider(TEnum @enum)
    {
        return @enum.ToString("G");
    }
}