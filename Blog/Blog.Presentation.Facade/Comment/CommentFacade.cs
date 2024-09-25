using Blog.Application.Comment.ChangeStatus;
using Blog.Application.Comment.Create;
using Blog.Application.Comment.Delete;
using Blog.Application.Comment.Edit;
using Blog.Query.Comment.DTOs;
using Blog.Query.Comment.GetByFilter;
using Blog.Query.Comment.GetByFilterProduct;
using Blog.Query.Comment.GetById;
using Common.Application;
using MediatR;

namespace Blog.Presentation.Facade.Comment;

    internal class CommentFacade : ICommentFacade
    {
        private readonly IMediator _mediator;

        public CommentFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> ChangeStatus(ChangeStatusCommentCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> CreateComment(CreateCommentCommand command)
        {
            return await _mediator.Send(command);

        }

        public async Task<OperationResult> EditComment(EditCommentCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> DeleteComment(DeleteCommentCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<CommentDto?> GetCommentById(long id)
        {
            return await _mediator.Send(new GetCommentByIdQuery(id));
        }

        public async Task<CommentFilterResult> GetCommentsByFilter(CommentFilterParam filterParams)
        {
            return await _mediator.Send(new GetCommentByFilterQuery(filterParams));
        }

        public async Task<CommentFilterResultProduct> GetCommentByFilterProduct(CommentFilterParamProduct filterParams)
        {
           return await _mediator.Send(new GetCommentByFilterProductQuery(filterParams));
        }
}