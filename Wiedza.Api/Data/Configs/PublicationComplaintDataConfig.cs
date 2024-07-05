using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wiedza.Api.Data.ValueGenerators;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;

namespace Wiedza.Api.Data.Configs;

internal class PublicationComplaintDataConfig : IEntityTypeConfiguration<PublicationComplaint>
{
    public void Configure(EntityTypeBuilder<PublicationComplaint> builder)
    {
        builder.Property(p => p.Status).HasDefaultValue(ComplaintStatus.New);

        builder.Property(p => p.Title).HasMaxLength(50);
        builder.Property(p => p.Description).HasMaxLength(500);
        builder.Property(p => p.CreatedAt).HasValueGenerator<DateTimeOffsetValueGenerator>();

        builder.HasOne(p => p.Author).WithMany().HasForeignKey(p => p.AuthorId);
        builder.HasOne(p => p.Administrator).WithMany().HasForeignKey(p => p.AdministratorId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(p => p.AttachmentFile).WithMany().HasForeignKey(p => p.AttachmentFileId).OnDelete(DeleteBehavior.Restrict);
    }
}