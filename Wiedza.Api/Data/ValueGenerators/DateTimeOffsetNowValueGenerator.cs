using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Wiedza.Api.Data.ValueGenerators;

internal class DateTimeOffsetNowValueGenerator : ValueGenerator<DateTimeOffset>
{
    private readonly TimeSpan? _step;

    public DateTimeOffsetNowValueGenerator()
    {
    }

    public DateTimeOffsetNowValueGenerator(TimeSpan step)
    {
        _step = step;
    }

    public override bool GeneratesTemporaryValues => false;

    public override DateTimeOffset Next(EntityEntry entry)
    {
        if (_step is null) return DateTimeOffset.UtcNow;
        return DateTimeOffset.UtcNow.Add(_step.Value);
    }
}