using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wiedza.Api.Data.ValueGenerators;
using Wiedza.Core.Models.Data.Base;

namespace Wiedza.Api.Data.Configs;

internal class UserDataConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.CreatedAt).HasValueGenerator<DateTimeOffsetNowValueGenerator>();

        builder.HasIndex(p => p.Username).IsUnique();
        builder.HasIndex(p => p.Email).IsUnique();

        builder.Property<string>("type").HasValueGenerator<TypeValueGenerator>();

        builder.UseTptMappingStrategy().ToTable("users");

    }
}