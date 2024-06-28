﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wiedza.Api.Data.ValueGenerators;
using Wiedza.Core.Models;
using Wiedza.Core.Models.Enums;

namespace Wiedza.Api.Data.Configs;

internal class PersonDataConfig : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.Property(p => p.CreatedAt).HasValueGenerator<DateTimeOffsetValueGenerator>();

        builder.HasIndex(p => p.Username).IsUnique();
        builder.HasIndex(p => p.Email).IsUnique();

        builder.Property(p => p.Name).HasMaxLength(50);
        builder.Property(p => p.Email).HasMaxLength(50);
        builder.Property(p => p.Description).HasMaxLength(500);
        builder.Property(p => p.Username).HasMaxLength(30);
        builder.Property(p => p.AccountState).HasDefaultValue(AccountState.Active);
    }
}