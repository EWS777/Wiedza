using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wiedza.Api.Data.ValueGenerators;
using Wiedza.Core.Models;

namespace Wiedza.Api.Data.Configs;

internal class VerificationDataConfig : IEntityTypeConfiguration<Verification>
{
    public void Configure(EntityTypeBuilder<Verification> builder)
    {
        builder.Property(p => p.CreatedAt).HasValueGenerator<DateTimeOffsetValueGenerator>();

        builder.Property(p => p.Name).HasMaxLength(50);
        builder.Property(p => p.Surname).HasMaxLength(50);
        builder.Property(p => p.ImageDocumentBytes).HasMaxLength(10 * 1024 * 1024);

        builder.HasOne(p => p.Person).WithMany().HasForeignKey(p => p.PersonId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(p => p.PersonId).IsUnique();
    }
}