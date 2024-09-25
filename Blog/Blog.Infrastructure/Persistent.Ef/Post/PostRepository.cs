using Blog.Domain.PostAgg.Repository;
using Blog.Infrastructure._Utilities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistent.Ef.Post
{
    public class PostRepository : BaseRepository<Domain.PostAgg.Post>, IPostRepository
    {
        public PostRepository(BlogContext context) : base(context)
        {
        }

        public async Task<bool> DeletePost(long postId)
        {
            var post = await Context.Posts.FirstOrDefaultAsync(i => i.Id == postId);
            if (post == null)
                return false;
            Context.Posts.Remove(post);
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
