using Common.Query;
using Common.Query.Filter;

namespace Blog.Query.Post.DTOs
{
    public class PostFilterDataDto : BaseDto
    {
        public string? ImageName { get; set; }
        public string? CategoryTitle { get; set; }
        public string? UserFullName { get; set; }
        public string? UserId { get; set; }
        public string? Title { get; set; }
        public int Visit { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public string? MetaDescription { get; set; }
    }
    public class PostFilterParam : BaseFilterParam
    {
        public string? Slug { get; set; } = "";
        public string? Search { get; set; } = "";
        public PostSearchOrderBy? SearchOrderBy { get; set; }
        public string? Title { get; set; }
        public long CategoryId { get; set; }

    }
    public class PostFilterResult : BaseFilter<PostFilterDataDto, PostFilterParam>
    {
    }

    public enum PostSearchOrderBy
    {
        visit,
        latest

    }
}
