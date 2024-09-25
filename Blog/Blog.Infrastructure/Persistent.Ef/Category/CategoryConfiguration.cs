using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Persistent.Ef.Category
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Domain.CategoryAgg.Category>
    {
        public void Configure(EntityTypeBuilder<Domain.CategoryAgg.Category> builder)
        {
            builder.ToTable("Categories", "dbo");

            builder.HasKey(c => c.Id);
            builder.HasIndex(b => b.Slug).IsUnique();

            builder.Property(b => b.Title).IsRequired().HasMaxLength(50);

        }
    }
}
