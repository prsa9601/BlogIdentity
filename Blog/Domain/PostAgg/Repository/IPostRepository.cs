using Common.Domain.Repository;

namespace Blog.Domain.PostAgg.Repository
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<bool> DeletePost(long postId);
    }
}
