namespace Wiedza.Core.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public sealed class EnumTypeValueAttribute : Attribute
{
    public Type Type { get; }

    public EnumTypeValueAttribute(Type type)
    {
        Type = type;
    }
}