using Blog.Query.User.DTOs;
using Common.Query;

namespace Blog.Query.User.GetByFilter;

public class GetUserByFilterQuery : QueryFilter<UserFilterResult, UserFilterParams>
{
    public GetUserByFilterQuery(UserFilterParams filterParams) : base(filterParams)
    {
    }
}