using Blog.Application.Comment.ChangeStatus;
using Blog.Application.Comment.Create;
using Blog.Application.Comment.Delete;
using Blog.Application.Comment.Edit;
using Blog.Query.Comment.DTOs;
using Common.Application;

namespace Blog.Presentation.Facade.Comment
{
    public interface ICommentFacade
    {
        Task<OperationResult> ChangeStatus(ChangeStatusCommentCommand command);
        Task<OperationResult> CreateComment(CreateCommentCommand command);
        Task<OperationResult> EditComment(EditCommentCommand command);
        Task<OperationResult> DeleteComment(DeleteCommentCommand command);


        Task<CommentDto?> GetCommentById(long id);
        Task<CommentFilterResult> GetCommentsByFilter(CommentFilterParam filterParams);
        Task<CommentFilterResultProduct> GetCommentByFilterProduct(CommentFilterParamProduct filterParams);
    }
}
