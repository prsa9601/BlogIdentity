using Blog.Infrastructure.Persistent.Ef;
using Blog.Query.User.DTOs;
using Common.Query;
using Microsoft.EntityFrameworkCore;

namespace Blog.Query.User.GetByFilter;

public class GetUserByFilterQueryHandler : IQueryHandler<GetUserByFilterQuery, UserFilterResult>
{
    private readonly BlogContext _context;

    public GetUserByFilterQueryHandler(BlogContext context)
    {
        _context = context;
    }

    public async Task<UserFilterResult> Handle(GetUserByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;
        var result = _context.Users.OrderByDescending(d => d.Id).AsQueryable();

        if (!string.IsNullOrWhiteSpace(@params.UserName))
            result = result.Where(r => r.UserName!.Contains(@params.UserName));
        
        if (!string.IsNullOrWhiteSpace(@params.Name))
            result = result.Where(r => r.Name.Contains(@params.Name));
        
        if (!string.IsNullOrWhiteSpace(@params.Family))
            result = result.Where(r => r.Family.Contains(@params.Family));

        if (!string.IsNullOrWhiteSpace(@params.PhoneNumber))
            result = result.Where(r => r.PhoneNumber!.Contains(@params.PhoneNumber));

        if (!string.IsNullOrWhiteSpace(@params.PhoneNumber))
            result = result.Where(r => r.Id == @params.Id);

        var skip = (@params.PageId - 1) * @params.Take;
        var model = new UserFilterResult()
        {
            Data = await result.Skip(skip).Take(@params.Take)
                .Select(user => user.MapFilterData()).ToListAsync(cancellationToken),
            FilterParams = @params
        };

        model.GeneratePaging(result, @params.Take, @params.PageId);
        return model;
    }
}