

using Blog.Query.Post.DTOs;
using Common.Application;
using Common.Query;

namespace Blog.Query.Post.GetByFilter
{
    public class GetPostByFilterQuery : QueryFilter<PostFilterResult , PostFilterParam>
    {
        public GetPostByFilterQuery(PostFilterParam filterParams) : base(filterParams)
        {
        }
    }
}
