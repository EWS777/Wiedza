using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wiedza.Api.Data.ValueGenerators;
using Wiedza.Core.Models;

namespace Wiedza.Api.Data.Configs;

internal class AttachmentFileDataConfig : IEntityTypeConfiguration<AttachmentFile>
{
    public void Configure(EntityTypeBuilder<AttachmentFile> builder)
    {
        builder.Property(p => p.UploadedAt).HasValueGenerator<DateTimeOffsetValueGenerator>();
    }
}