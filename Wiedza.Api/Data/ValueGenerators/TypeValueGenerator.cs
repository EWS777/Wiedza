using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Wiedza.Api.Data.ValueGenerators;

public class TypeValueGenerator : ValueGenerator<Type>
{
    public override Type Next(EntityEntry entry) => entry.Entity.GetType();

    public override bool GeneratesTemporaryValues => false;
}