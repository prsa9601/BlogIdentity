using Common.Domain.Repository;

namespace Blog.Domain.CommentAgg.Repository
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        Task<bool> DeleteComment(long commentId);
    }
}
