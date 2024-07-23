using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Wiedza.Api.Data.ValueConvertors;

public class TypeValueConverter : ValueConverter<Type, string>
{
    public new static Expression<Func<Type, string>> ConvertToProviderExpression => type => type.FullName ?? type.Name;
    public new static Expression<Func<string, Type>> ConvertFromProviderExpression => s => Type.GetType(s)!;

    public TypeValueConverter() : base(ConvertToProviderExpression, ConvertFromProviderExpression) { }
}