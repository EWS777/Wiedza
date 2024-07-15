using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wiedza.Api.Data.ValueGenerators;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;

namespace Wiedza.Api.Data.Configs;

internal class MessageComplaintDataConfig : IEntityTypeConfiguration<MessageComplaint>
{
    public void Configure(EntityTypeBuilder<MessageComplaint> builder)
    {
        builder.Property(p => p.Status).HasDefaultValue(ComplaintStatus.New);

        builder.Property(p => p.Title).HasMaxLength(50);
        builder.Property(p => p.Description).HasMaxLength(500);
        builder.Property(p => p.CreatedAt).HasValueGenerator<DateTimeOffsetValueGenerator>();

        builder.HasOne(p => p.Author).WithMany().HasForeignKey(p => p.AuthorId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(p => p.Administrator).WithMany().HasForeignKey(p => p.AdministratorId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(p => p.Message).WithMany().HasForeignKey(p => p.MessageId).OnDelete(DeleteBehavior.ClientCascade);
    }
}