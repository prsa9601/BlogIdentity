using Blog.Domain.CommentAgg;
using Blog.Infrastructure.Persistent.Ef;
using Blog.Query.Comment.DTOs;
using Common.Query;
using Microsoft.EntityFrameworkCore;

namespace Blog.Query.Comment.GetByFilterProduct
{
    public class GetCommentByFilterProductQuery : QueryFilter<CommentFilterResultProduct, CommentFilterParamProduct>
    {
        public GetCommentByFilterProductQuery(CommentFilterParamProduct filterParams) : base(filterParams)
        {
        }
    }
    public class GetCommentByFilterProductQueryHandler : IQueryHandler<GetCommentByFilterProductQuery, CommentFilterResultProduct>
    {
        private readonly BlogContext _context;

        public GetCommentByFilterProductQueryHandler(BlogContext context)
        {
            _context = context;
        }


        public async Task<CommentFilterResultProduct> Handle(GetCommentByFilterProductQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;

            var result = _context.Comments.OrderByDescending(d => d.CreationDate).AsQueryable();


            if (@params.PostId != null)
                result = result.Where(r => r.PostId == @params.PostId);


            var skip = (@params.PageId - 1) * @params.Take;
            var model = new CommentFilterResultProduct()
            {
                Data = await result.Skip(skip).Take(@params.Take)
                    .Select(comment => comment.MapFilterCommentProduct(_context))
                    .ToListAsync(cancellationToken),
                FilterParams = @params
            };
            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }
    }
}
