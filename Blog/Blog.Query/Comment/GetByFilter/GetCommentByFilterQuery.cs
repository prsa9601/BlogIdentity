using Blog.Domain.CommentAgg;
using Blog.Infrastructure.Persistent.Ef;
using Blog.Query.Comment.DTOs;
using Common.Query;
using Microsoft.EntityFrameworkCore;

namespace Blog.Query.Comment.GetByFilter
{
    public class GetCommentByFilterQuery : QueryFilter<CommentFilterResult, CommentFilterParam>
    {
        public GetCommentByFilterQuery(CommentFilterParam filterParams) : base(filterParams)
        {
        }
    }
    public class GetCommentByFilterQueryHandler : IQueryHandler<GetCommentByFilterQuery, CommentFilterResult>
    {
        private readonly BlogContext _context;

        public GetCommentByFilterQueryHandler(BlogContext context)
        {
            _context = context;
        }

        public async Task<CommentFilterResult> Handle(GetCommentByFilterQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;

            var result = _context.Comments.OrderByDescending(d => d.CreationDate).AsQueryable();


            if (@params.PostId != 0)
                result = result.Where(r => r.PostId == @params.PostId);

            switch (@params.CommentStatus)
            {
                case CommentStatus.Accepted:
                {
                    result = result.OrderByDescending(r => r.Status == CommentStatus.Accepted);
                        break;
                }
                case CommentStatus.Pending:
                {
                    result = result.Where(r => r.Status == CommentStatus.Pending);
                        break;
                }
                case CommentStatus.Rejected:
                {
                    result = result.Where(r => r.Status == CommentStatus.Rejected);
                        break;
                }

            }
           

            if (@params.UserId != null)
                result = result.Where(r => r.UserId == @params.UserId);

            //remind
            if (@params.StartDate != default(DateTime))
                result = result.Where(r => r.CreationDate.Date >= @params.StartDate.Date);

            if (@params.EndDate != default(DateTime))
                result = result.Where(r => r.CreationDate.Date <= @params.EndDate.Date);



            var skip = (@params.PageId - 1) * @params.Take;
            var model = new CommentFilterResult()
            {
                Data = await result.Skip(skip).Take(@params.Take)
                    .Select(comment => comment.MapFilterComment())
                    .ToListAsync(cancellationToken),
                FilterParams = @params
            };
            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }
    }
}
