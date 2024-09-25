using Blog.Application.Post.AddImage;
using Blog.Application.Post.Create;
using Blog.Application.Post.Delete;
using Blog.Application.Post.Edit;
using Blog.Application.Post.Show;
using Blog.Query.Post.DTOs;
using Common.Application;

namespace Blog.Presentation.Facade.Post
{
    public interface IPostFacade
    {
        Task<OperationResult> CreatePost(CreatePostCommand command);
        Task<OperationResult> AddImagePost(AddImageCommand command);
        Task<OperationResult> DeletePost(DeletePostCommand command);
        Task<OperationResult> VisitPost(ShowPostCommand command);
        Task<OperationResult> EditPost(EditPostCommand command);

        Task<PostDto?> GetPostById(long postId);
        Task<PostDto?> GetPostBySlug(string slug);
        Task<PostFilterResult?> GetPosts(PostFilterParam param);
    }
}
