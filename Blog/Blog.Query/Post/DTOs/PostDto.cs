
using Common.Query;

namespace Blog.Query.Post.DTOs
{
    public class PostDto : BaseDto
    {
        public string UserId { get; set; } = String.Empty;
        public string UserFullName { get; set; } = String.Empty;
        //  public string CategoryTitle { get; set; }       
        public long CategoryId { get; set; }
        public string Title { get; set; } = String.Empty;
        public string ImageName { get; set; } = String.Empty;
        public string Slug { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string MetaDescription { get; set; } = String.Empty;
        public int Visit { get; set; }

    }
}
