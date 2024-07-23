using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wiedza.Api.Data.ValueConvertors;
using Wiedza.Api.Data.ValueGenerators;
using Wiedza.Core.Models.Data.Base;
using Wiedza.Core.Models.Enums;

namespace Wiedza.Api.Data.Configs;

internal class UserDataConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.CreatedAt).HasValueGenerator<DateTimeOffsetNowValueGenerator>();

        builder.HasIndex(p => p.Username).IsUnique();
        builder.Property(p => p.Username).HasMaxLength(30);

        builder.HasIndex(p => p.Email).IsUnique();
        builder.Property(p => p.Email).HasMaxLength(50);

        builder.Property(p => p.AccountState).HasDefaultValue(AccountState.Active);

        builder.Property(p=>p.UserType)
            .HasValueGenerator<TypeValueGenerator>().ValueGeneratedOnAdd()
            .HasConversion<TypeValueConverter>();

        builder.UseTptMappingStrategy();

    }
}