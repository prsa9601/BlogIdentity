using Blog.Infrastructure.Persistent.Ef;
using Blog.Query.User.DTOs;
using Common.Query;
using Microsoft.EntityFrameworkCore;

namespace Blog.Query.User.GetById;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto?>
{
    private readonly BlogContext _context;

    public GetUserByIdQueryHandler(BlogContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(f => f.Id == request.UserId, cancellationToken);
        if (user == null)
            return null;


        return await user.Map().SetUserRoleTitles(_context);
    }
}