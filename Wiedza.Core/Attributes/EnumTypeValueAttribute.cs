namespace Wiedza.Core.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public sealed class EnumTypeValueAttribute : Attribute
{
    public EnumTypeValueAttribute(Type type)
    {
        Type = type;
    }

    public Type Type { get; }
}