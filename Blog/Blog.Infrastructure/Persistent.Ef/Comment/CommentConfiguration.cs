using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Persistent.Ef.Comment
{
    internal class CommentConfiguration : IEntityTypeConfiguration<Domain.CommentAgg.Comment>
    {
        public void Configure(EntityTypeBuilder<Domain.CommentAgg.Comment> builder)
        {
            builder.ToTable("Comments", "dbo");
            builder.HasIndex(b => b.Id);
            builder.HasIndex(b => b.PostId);
            builder.HasIndex(b => b.UserId);


        }
    }
}
