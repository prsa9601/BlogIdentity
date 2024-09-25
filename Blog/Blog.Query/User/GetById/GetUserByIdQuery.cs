using Blog.Query.User.DTOs;
using Common.Query;

namespace Blog.Query.User.GetById;

public class GetUserByIdQuery : IQuery<UserDto?>
{
    public GetUserByIdQuery(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; private set; }
}