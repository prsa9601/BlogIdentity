using Blog.Api.Infrastructure.Security;
using Blog.Application.Comment.ChangeStatus;
using Blog.Application.Comment.Create;
using Blog.Application.Comment.Delete;
using Blog.Application.Comment.Edit;
using Blog.Domain.CommentAgg;
using Blog.Domain.RoleAgg.Enums;
using Blog.Presentation.Facade.Comment;
using Blog.Query.Comment.DTOs;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ApiController
    {
        private readonly ICommentFacade _commentFacade;

        public CommentController(ICommentFacade commentFacade)
        {
            _commentFacade = commentFacade;
        }

        [PermissionChecker(Permission.Comment_Management)]
        [HttpGet]
        public async Task<ApiResult<CommentFilterResult>> GetCommentByFilter([FromQuery] CommentFilterParam filterParams)
        {
            var result = await _commentFacade.GetCommentsByFilter(filterParams);
            return QueryResult(result);
        }
        [HttpGet("productComments")]
        public async Task<ApiResult<CommentFilterResult>> GetPostComments(int pageId = 1, int take = 10, long postId = 0)
        {
            var result = await _commentFacade.GetCommentsByFilter(new CommentFilterParam()
            {
                PostId = postId,
                PageId = pageId,
                Take = take,
                CommentStatus = CommentStatus.Accepted
            });
            return QueryResult(result);
        }
        [HttpGet("getCommentByFilterProduct")]
        public async Task<ApiResult<CommentFilterResultProduct>> GetCommentByFilterProduct(int pageId = 1, int take = 10, long postId = 0)
        {
            var result = await _commentFacade.GetCommentByFilterProduct(new CommentFilterParamProduct()
            {
                PostId = postId,
                PageId = pageId,
                Take = take,
                //CommentStatus = CommentStatus.Accepted
            });
            return QueryResult(result);
        }

        [PermissionChecker(Permission.Comment_Management)]
        [HttpGet("{commentId}")]
        public async Task<ApiResult<CommentDto?>> GetCommentById(long commentId)
        {
            var result = await _commentFacade.GetCommentById(commentId);
            return QueryResult(result);
        }

        [HttpPost]
       // [Authorize]
        public async Task<ApiResult> CreateComment(CreateCommentCommand command)
        {
            var result = await _commentFacade.CreateComment(command);
            return CommandResult(result);
        }

        [HttpPut]
        [Authorize]
        public async Task<ApiResult> EditComment(EditCommentCommand command)
        {
            var result = await _commentFacade.EditComment(command);
            return CommandResult(result);
        }

        [HttpPut("changeStatus")]
        [PermissionChecker(Permission.Comment_Management)]
        public async Task<ApiResult> ChangeCommentStatus(ChangeStatusCommentCommand command)
        {
            var result = await _commentFacade.ChangeStatus(command);
            return CommandResult(result);
        }

        [HttpDelete("{commentId}")]
        [Authorize]
        public async Task<ApiResult> DeleteComment(long commentId)
        {
            var result = await _commentFacade.DeleteComment(new DeleteCommentCommand(commentId));
            return CommandResult(result);
        }
    }
}
