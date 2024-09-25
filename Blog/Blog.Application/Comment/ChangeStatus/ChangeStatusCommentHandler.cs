using Blog.Domain.CommentAgg.Repository;
using Common.Application;

namespace Blog.Application.Comment.ChangeStatus
{
    public class ChangeStatusCommentHandler : IBaseCommandHandler<ChangeStatusCommentCommand>
    {
        private readonly ICommentRepository _repository;

        public ChangeStatusCommentHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(ChangeStatusCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _repository.GetTracking(request.Id);
            if (comment == null) 
                return OperationResult.NotFound();
            comment.ChangeStatus(request.Status);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
