using Blog.Domain.PostAgg.Repository;
using Common.Application;

namespace Blog.Application.Post.Delete
{
    public class DeletePostCommandHandler : IBaseCommandHandler<DeletePostCommand>
    {
        private readonly IPostRepository _repository;
        public DeletePostCommandHandler(IPostRepository repository)
        {
            _repository = repository;
        }
        public async Task<OperationResult> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.DeletePost(request.postId);
            if (!result)
                return OperationResult.Error();
            return OperationResult.Success();
        }
    }
}
