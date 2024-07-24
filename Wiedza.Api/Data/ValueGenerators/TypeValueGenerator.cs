using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Wiedza.Api.Core.Extensions;
using Wiedza.Core.Attributes;

namespace Wiedza.Api.Data.ValueGenerators;

public class TypeValueGenerator<TEnum> : ValueGenerator<TEnum> where TEnum : Enum
{
    public override TEnum Next(EntityEntry entry)
    {
        var type = entry.Entity.GetType();
        var values = Enum.GetValues(typeof(TEnum));
        foreach (var value in values.OfType<TEnum>())
        {
            var attribute = value.GetEnumAttribute<TEnum,EnumTypeValueAttribute>();
            if (attribute?.Type == type) return value;
        }

        throw new ArgumentException($"Enum values doesn't have a {nameof(EnumTypeValueAttribute)} with type `{type.AssemblyQualifiedName}`");
    }

    public override bool GeneratesTemporaryValues => false;
}