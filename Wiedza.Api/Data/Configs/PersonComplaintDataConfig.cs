using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wiedza.Core.Models.Data;

namespace Wiedza.Api.Data.Configs;

internal class PersonComplaintDataConfig : IEntityTypeConfiguration<PersonComplaint>
{
    public void Configure(EntityTypeBuilder<PersonComplaint> builder)
    {
        builder.HasOne(p => p.Person).WithMany().HasForeignKey(p => p.PersonId).OnDelete(DeleteBehavior.ClientCascade);
    }
}