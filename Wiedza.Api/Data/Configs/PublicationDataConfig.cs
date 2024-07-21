using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wiedza.Api.Data.ValueGenerators;
using Wiedza.Core.Models.Data.Base;
using Wiedza.Core.Models.Enums;

namespace Wiedza.Api.Data.Configs;

internal class PublicationDataConfig : IEntityTypeConfiguration<Publication>
{
    public void Configure(EntityTypeBuilder<Publication> builder)
    {
        builder.Property(p => p.CreatedAt).HasValueGenerator<DateTimeOffsetNowValueGenerator>();
        builder.Property(p => p.ExpiresAt).HasValueGenerator((_, _) =>
        {
            var generator = new DateTimeOffsetNowValueGenerator(TimeSpan.FromDays(30));
            return generator;
        });

        builder.HasOne(p => p.Author).WithMany().HasForeignKey(p => p.AuthorId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.ClientCascade);
        builder.Property(p => p.Status).HasDefaultValue(PublicationStatus.Pending);
        builder.Property(p => p.Title).HasMaxLength(50);
        builder.Property(p => p.Description).HasMaxLength(500);

        builder.Property<string>("type").HasValueGenerator<TypeValueGenerator>();

        builder.UseTptMappingStrategy().ToTable("publications");
    }
}