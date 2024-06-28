using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Wiedza.Api.Data.ValueGenerators;

internal class DateTimeOffsetValueGenerator : ValueGenerator<DateTimeOffset>
{
    public override bool GeneratesTemporaryValues => false;

    public override DateTimeOffset Next(EntityEntry entry)
    {
        return DateTimeOffset.UtcNow;
    }
}