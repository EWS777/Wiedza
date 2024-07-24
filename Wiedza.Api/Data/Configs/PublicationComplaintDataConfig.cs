using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wiedza.Core.Models.Data;

namespace Wiedza.Api.Data.Configs;

internal class PublicationComplaintDataConfig : IEntityTypeConfiguration<PublicationComplaint>
{
    public void Configure(EntityTypeBuilder<PublicationComplaint> builder)
    {
        builder.HasOne(p => p.Publication).WithMany()
            .HasForeignKey(p => p.PublicationId).OnDelete(DeleteBehavior.ClientCascade);
    }
}