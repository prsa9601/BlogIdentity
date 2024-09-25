using Common.Application;

namespace Blog.Application.Comment.Edit
{
    public record EditCommentCommand(long CommentId, string Text, long UserId) : IBaseCommand;

}
