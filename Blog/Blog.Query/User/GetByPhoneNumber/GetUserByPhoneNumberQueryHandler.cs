using Common.Query;
using Microsoft.EntityFrameworkCore;
using Blog.Infrastructure.Persistent.Ef;
using Blog.Query.User.DTOs;

namespace Blog.Query.User.GetByPhoneNumber;

public class GetUserByPhoneNumberQueryHandler : IQueryHandler<GetUserByPhoneNumberQuery, UserDto?>
{
    private readonly BlogContext _context;

    public GetUserByPhoneNumberQueryHandler(BlogContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> Handle(GetUserByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(f => f.PhoneNumber == request.PhoneNumber, cancellationToken);

        if (user == null)
            return null;


        return await user.Map().SetUserRoleTitles(_context);
    }
}