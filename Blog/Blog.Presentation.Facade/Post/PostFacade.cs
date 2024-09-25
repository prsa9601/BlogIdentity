using Blog.Application.Post.AddImage;
using Blog.Application.Post.Create;
using Blog.Application.Post.Delete;
using Blog.Application.Post.Edit;
using Blog.Application.Post.Show;
using Blog.Query.Post.DTOs;
using Blog.Query.Post.GetBtId;
using Blog.Query.Post.GetByFilter;
using Blog.Query.Post.GetBySlug;
using Common.Application;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Blog.Presentation.Facade.Post
{
    public class PostFacade : IPostFacade
    {
        private readonly IMediator _mediator;

        public PostFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> CreatePost(CreatePostCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> AddImagePost(AddImageCommand command)
        {
            return await _mediator.Send(command);

        }

        public async Task<OperationResult> VisitPost(ShowPostCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> EditPost(EditPostCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<PostDto?> GetPostById(long postId)
        {
            return await _mediator.Send(new GetPostByIdQuery(postId));
        }

        public async Task<PostDto?> GetPostBySlug(string slug)
        {
            return await _mediator.Send(new GetPostBySlugQuery(slug));
        }

        public async Task<PostFilterResult?> GetPosts(PostFilterParam param)
        {
            return await _mediator.Send(new GetPostByFilterQuery(param));
        }

        public async Task<OperationResult> DeletePost(DeletePostCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
