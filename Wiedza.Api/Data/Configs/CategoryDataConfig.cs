using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wiedza.Core.Models;

namespace Wiedza.Api.Data.Configs;

internal class CategoryDataConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasOne(p => p.ParentCategory).WithMany().HasForeignKey(p => p.ParentCategoryId).OnDelete(DeleteBehavior.Restrict);
    }
}