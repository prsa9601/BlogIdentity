using Blog.Infrastructure.Persistent.Ef;
using Blog.Query.Comment.DTOs;
using Common.Query;
using Microsoft.EntityFrameworkCore;

namespace Blog.Query.Comment.GetById
{
    public record class GetCommentByIdQuery(long commentId) : IQuery<CommentDto?>;
    
    public class GetCommentByIdQueryHandler : IQueryHandler<GetCommentByIdQuery, CommentDto?>
    {
        private readonly BlogContext _context;

        public GetCommentByIdQueryHandler(BlogContext context)
        {
            _context = context;
        }

        public async Task<CommentDto?> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
                  
            var comment = await _context.Comments.FirstOrDefaultAsync(f => f.Id == request.commentId, cancellationToken: cancellationToken);
            return comment.Map(); 
        }
    }
}
