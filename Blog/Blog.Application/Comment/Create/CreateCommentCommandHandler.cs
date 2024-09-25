using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.CommentAgg.Repository;
using Common.Application;

namespace Blog.Application.Comment.Create
{
    public class CreateCommentCommandHandler : IBaseCommandHandler<CreateCommentCommand>
    {
        private readonly ICommentRepository _repository;

        public CreateCommentCommandHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new Domain.CommentAgg.Comment(request.UserId, request.ProductId, request.Text);
            _repository.Add(comment);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
