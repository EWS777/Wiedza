using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Wiedza.Api.Data.ValueGenerators;

public class TypeValueGenerator : ValueGenerator<string>
{
    public override string Next(EntityEntry entry) => entry.Entity.GetType().Name;

    public override bool GeneratesTemporaryValues => false;
}