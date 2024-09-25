using Blog.Query.Post.DTOs;
using Common.Query;

namespace Blog.Query.Post.GetBtId
{
    public record class GetPostByIdQuery(long PostId) : IQuery<PostDto?>;

}
