using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wiedza.Api.Data.ValueGenerators;
using Wiedza.Core.Models;

namespace Wiedza.Api.Data.Configs;

internal class ReviewDataConfig : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.Property(p => p.CreatedAt).HasValueGenerator<DateTimeOffsetValueGenerator>();

        builder.Property(p => p.Message).HasMaxLength(200);

        builder.HasOne(p => p.Author).WithMany().HasForeignKey(p => p.AuthorId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(p => p.Person).WithMany().HasForeignKey(p => p.PersonId).OnDelete(DeleteBehavior.Restrict);
    }
}