using Blog.Domain.CommentAgg.Repository;
using Blog.Infrastructure._Utilities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistent.Ef.Comment
{
    public class CommentRepository : BaseRepository<Domain.CommentAgg.Comment>, ICommentRepository
    {
        public CommentRepository(BlogContext context) : base(context)
        {
        }

        public async Task<bool> DeleteComment(long commentId)
        {
            var comment = await Context.Comments.FirstOrDefaultAsync(i => i.Id == commentId);
            if (comment == null)
                return false;
            Context.Comments.Remove(comment);
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
