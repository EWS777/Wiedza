using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wiedza.Api.Data.ValueGenerators;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;

namespace Wiedza.Api.Data.Configs;

internal class WithdrawDataConfig : IEntityTypeConfiguration<Withdraw>
{
    public void Configure(EntityTypeBuilder<Withdraw> builder)
    {
        builder.Property(p => p.CreatedAt).HasValueGenerator<DateTimeOffsetValueGenerator>();
        builder.Property(p => p.Status).HasDefaultValue(WithdrawStatus.New);

        builder.HasOne(p => p.Person).WithMany().HasForeignKey(p => p.PersonId).OnDelete(DeleteBehavior.ClientCascade);
    }
}