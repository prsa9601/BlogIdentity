using Blog.Domain.CommentAgg;
using Common.Application;

namespace Blog.Application.Comment.ChangeStatus
{
    public record ChangeStatusCommentCommand(long Id, CommentStatus Status) : IBaseCommand;

}
