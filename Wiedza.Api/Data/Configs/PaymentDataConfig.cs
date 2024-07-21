using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wiedza.Api.Data.ValueGenerators;
using Wiedza.Core.Models.Data;

namespace Wiedza.Api.Data.Configs;

internal class PaymentDataConfig : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.Property(p => p.CreatedAt).HasValueGenerator<DateTimeOffsetNowValueGenerator>();
        builder.Property(p => p.Email).HasMaxLength(50);
        builder.Property(p => p.Name).HasMaxLength(50);
        builder.Property(p => p.Surname).HasMaxLength(50);
        builder.Property(p => p.Street).HasMaxLength(50);
        builder.Property(p => p.Country).HasMaxLength(50);
        builder.Property(p => p.City).HasMaxLength(50);

        builder.HasOne(p => p.Person).WithMany().HasForeignKey(p => p.PersonId).OnDelete(DeleteBehavior.ClientCascade);
    }
}