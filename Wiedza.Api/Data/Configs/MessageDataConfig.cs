using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wiedza.Api.Data.ValueGenerators;
using Wiedza.Core.Models.Data;

namespace Wiedza.Api.Data.Configs;

internal class MessageDataConfig : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.Property(p => p.SendedAt).HasValueGenerator<DateTimeOffsetNowValueGenerator>();

        builder.HasOne(p => p.Author).WithMany().HasForeignKey(p => p.AuthorId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(p => p.Chat).WithMany().HasForeignKey(p => p.ChatId);
    }
}