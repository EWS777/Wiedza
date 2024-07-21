using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Data.Base;

namespace Wiedza.Api.Data.Configs;

public class ServiceDataConfig : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasBaseType<Publication>().ToTable("services");
    }
}