using Blog.Query.Role.DTOs;
using Common.Query;
using Blog.Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;

namespace Blog.Query.Role.GetByFilter
{
    public class GetRoleByFilterQuery : QueryFilter<RoleFilterResult, RoleFilterParam>
    {
        public GetRoleByFilterQuery(RoleFilterParam filterParams) : base(filterParams)
        {
        }
    }
    public class GetRoleByFilterQueryHandler : IQueryHandler<GetRoleByFilterQuery, RoleFilterResult>
    {
        private readonly BlogContext _context;

        public GetRoleByFilterQueryHandler(BlogContext context)
        {
            _context = context;
        }

        public async Task<RoleFilterResult> Handle(GetRoleByFilterQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;
            var result = _context.Roles.OrderByDescending(d => d.Id).AsQueryable();

            if (!string.IsNullOrWhiteSpace(@params.Name))
                result = result.Where(r => r.Name!.Contains(@params.Name));

            var skip = (@params.PageId - 1) * @params.Take;
            var model = new RoleFilterResult()
            {
                Data = await result.Skip(skip).Take(@params.Take).Select(s => s.MapListData())
                    .ToListAsync(cancellationToken),
                FilterParams = @params
            };
            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }
    }
}
