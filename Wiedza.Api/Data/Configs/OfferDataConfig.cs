using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wiedza.Api.Data.ValueGenerators;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;

namespace Wiedza.Api.Data.Configs;

internal class OfferDataConfig : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.Property(p => p.CreatedAt).HasValueGenerator<DateTimeOffsetNowValueGenerator>();
        builder.Property(p => p.Status).HasDefaultValue(OfferStatus.New);
        builder.Property(p => p.Message).HasMaxLength(200);

        builder.HasOne(p => p.Publication).WithMany().HasForeignKey(p => p.PulicationId).OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(p => p.Person).WithMany().HasForeignKey(p => p.PersonId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}