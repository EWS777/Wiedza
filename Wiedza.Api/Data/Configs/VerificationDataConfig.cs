using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wiedza.Api.Data.ValueGenerators;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;

namespace Wiedza.Api.Data.Configs;

internal class VerificationDataConfig : IEntityTypeConfiguration<Verification>
{
    public void Configure(EntityTypeBuilder<Verification> builder)
    {
        builder.Property(p => p.CreatedAt).HasValueGenerator<DateTimeOffsetNowValueGenerator>();

        builder.Property(p => p.Name).HasMaxLength(50);
        builder.Property(p => p.Surname).HasMaxLength(50);
        builder.Property(p => p.ImageDocumentBytes).HasMaxLength(10 * 1024 * 1024);
        builder.Property(p => p.Status).HasDefaultValue(VerificationStatus.New);

        builder.HasOne(p => p.Person).WithMany().HasForeignKey(p => p.PersonId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(p => p.Administrator).WithMany().HasForeignKey(p => p.AdministratorId);
        builder.HasIndex(p => p.PersonId).IsUnique();
    }
}