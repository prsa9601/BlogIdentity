using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.CommentAgg.Repository;
using Common.Application;

namespace Blog.Application.Comment.Edit
{
    public class EditCommentCommandHandler : IBaseCommandHandler<EditCommentCommand>
    {
        private readonly ICommentRepository _repository;

        public EditCommentCommandHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(EditCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _repository.GetTracking(request.CommentId);
            if (comment == null) 
                return OperationResult.NotFound();
            comment.Edit(request.Text);
            return OperationResult.Success();
        }
    }
}
