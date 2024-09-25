using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.PostAgg.Repository;
using Common.Application;

namespace Blog.Application.Post.Show
{
    public class ShowPostCommandHandler : IBaseCommandHandler<ShowPostCommand>
    {
        private readonly IPostRepository _repository;

        public ShowPostCommandHandler(IPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(ShowPostCommand request, CancellationToken cancellationToken)
        {
            var post = await _repository.GetTracking(request.postId);
            if (post == null) 
                return OperationResult.NotFound();
            post.VisitPost();
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
