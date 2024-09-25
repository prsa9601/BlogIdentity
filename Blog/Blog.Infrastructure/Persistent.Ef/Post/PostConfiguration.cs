using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Persistent.Ef.Post
{
    internal class PostConfiguration : IEntityTypeConfiguration<Domain.PostAgg.Post>
    {
        public void Configure(EntityTypeBuilder<Domain.PostAgg.Post> builder)
        {
            builder.ToTable("Posts", "dbo");

            builder.HasIndex(b => b.Id).IsUnique();

            builder.Property(b => b.Title).HasMaxLength(200);

            builder.Property(b => b.Description);
        }
    }
}
