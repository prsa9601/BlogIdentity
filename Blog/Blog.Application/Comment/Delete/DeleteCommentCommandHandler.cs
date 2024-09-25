using Blog.Application.Comment.Delete;
using Blog.Domain.CommentAgg.Repository;
using Common.Application;

public class DeleteCommentCommandHandler : IBaseCommandHandler<DeleteCommentCommand>
{
    private readonly ICommentRepository _repository;
    public DeleteCommentCommandHandler(ICommentRepository repository)
    {
        _repository = repository;
    }
    public async Task<OperationResult> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.DeleteComment(request.commentId);
        if (!result)
            return OperationResult.Error();
        return OperationResult.Success();
    }
}
