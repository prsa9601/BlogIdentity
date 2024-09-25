using Blog.Infrastructure.Persistent.Ef;
using Blog.Query.Post.DTOs;
using Blog.Query.Role.DTOs;
using Blog.Query.Role;
using Common.Query;
using Microsoft.EntityFrameworkCore;

namespace Blog.Query.Post.GetByFilter
{
    public class GetPostByFilterQueryHandler : IQueryHandler<GetPostByFilterQuery , PostFilterResult>
    {
        private readonly BlogContext _context;

        public GetPostByFilterQueryHandler(BlogContext context)
        {
            _context = context;
        }

        public async Task<PostFilterResult> Handle(GetPostByFilterQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;
            var result = _context.Posts.OrderByDescending(d => d.Id).AsQueryable();

            if (!string.IsNullOrWhiteSpace(@params.Slug))
                result = result.Where(r => r.Slug == @params.Slug);

            if (!string.IsNullOrWhiteSpace(@params.Title))
                result = result.Where(r => r.Title.Contains(@params.Title));
            
            if (!string.IsNullOrWhiteSpace(@params.Search))
                result = result.Where(r => r.Title.Contains(@params.Search)||r.Slug.Contains(@params.Search)||r.Title.Contains(@params.Search));
           
            if (@params.CategoryId !=0)
                result = result.Where(r => r.CategoryId == @params.CategoryId);

            switch (@params.SearchOrderBy)
            {
                case PostSearchOrderBy.latest:
                {
                    result = result.OrderByDescending(r => r.CreationDate);
                    break;
                }
                case PostSearchOrderBy.visit:
                {
                    result = result.OrderByDescending(r => r.Visit);
                    break;
                }
            }

            var skip = (@params.PageId - 1) * @params.Take;
            var model = new PostFilterResult()
            {
                Data = await result.Skip(skip).Take(@params.Take).Select(s => s.MapListData(_context))
                    .ToListAsync(cancellationToken),
                FilterParams = @params
            };
            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }
    }
}
