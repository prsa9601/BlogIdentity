using Blog.Application.Post.AddImage;
using Blog.Application.Post.Create;
using Blog.Application.Post.Delete;
using Blog.Application.Post.Edit;
using Blog.Application.Post.Show;
using Blog.Application.Role.Delete;
using Blog.Presentation.Facade.Post;
using Blog.Query.Post.DTOs;
using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PostController : ApiController
    {
        private readonly IPostFacade _facade;
        public PostController(IPostFacade facade)
        {
            _facade = facade;
        }
        [HttpPost]
        public async Task<ApiResult> CreatePost([FromForm]CreatePostCommand command)
        {
            var result = await _facade.CreatePost(new CreatePostCommand()
            {
                ImageFile = command.ImageFile,
                Title = command.Title,
                CategoryId = command.CategoryId,
                Description = command.Description,
                Slug = command.Slug,
                UserId = command.UserId,
                MetaDescription = command.MetaDescription
                
            });
            return CommandResult(result);
        }
        [HttpPost("addImage")]
        public async Task<ApiResult> AddImagePost([FromForm]AddImageCommand command)
        {
            var result = await _facade.AddImagePost(command);
            return CommandResult(result);
        }
        //public async Task<ApiResult> VisitPost(ShowPostCommand command)
        //{
        //    var result = await _facade.VisitPost(command);

        //}
        [HttpPatch]
        public async Task<ApiResult> EditPost([FromForm]EditPostCommand command)
        {
            var result = await _facade.EditPost(command);
            return CommandResult(result);
        }
        [HttpGet("getById/{id}")]
        public async Task<ApiResult<PostDto?>> GetPostById(long id)
        {
            var result = await _facade.VisitPost(new ShowPostCommand(id));
            var getResult = await _facade.GetPostById(id); 
            return QueryResult(getResult);
        }
          [HttpGet("getPostByIdInBack/{id}")]
        public async Task<ApiResult<PostDto?>> GetPostByIdInBack(long id)
        {
            var getResult = await _facade.GetPostById(id); 
            return QueryResult(getResult);
        }
        [HttpGet("getBySlug/{slug}")]
        public async Task<ApiResult<PostDto?>> GetPostBySlug(string slug)
        {
            var getResult = await _facade.GetPostBySlug(slug);
            if (getResult != null)
            {
                var result = await _facade.VisitPost(new ShowPostCommand(getResult.Id));
            }

            return QueryResult(getResult);
        }
        [HttpGet]
        public async Task<ApiResult<PostFilterResult?>> GetPosts([FromQuery]PostFilterParam param)
        {
            var result = await _facade.GetPosts(param);
            return QueryResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<ApiResult> DeletePost(long id)
        {
            var result = await _facade.DeletePost(new DeletePostCommand(id));
            return CommandResult(result);
        }
    }
}
