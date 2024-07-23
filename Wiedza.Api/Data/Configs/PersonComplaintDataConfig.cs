﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wiedza.Api.Data.ValueGenerators;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;

namespace Wiedza.Api.Data.Configs;

internal class PersonComplaintDataConfig : IEntityTypeConfiguration<PersonComplaint>
{
    public void Configure(EntityTypeBuilder<PersonComplaint> builder)
    {
        builder.Property(p => p.Status).HasDefaultValue(ComplaintStatus.New);

        builder.Property(p => p.Title).HasMaxLength(50);
        builder.Property(p => p.Description).HasMaxLength(500);
        builder.Property(p => p.CreatedAt).HasValueGenerator<DateTimeOffsetNowValueGenerator>();

        builder.HasOne(p => p.Author).WithMany().HasForeignKey(p => p.AuthorId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(p => p.Person).WithMany().HasForeignKey(p => p.PersonId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(p => p.Administrator).WithMany().HasForeignKey(p => p.AdministratorId);
        builder.HasOne(p => p.AttachmentFile).WithMany().HasForeignKey(p => p.AttachmentFileId);
    }
}