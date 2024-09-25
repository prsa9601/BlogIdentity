using Common.Query;

namespace Blog.Query.Category.DTOs
{
    public class CategoryDto : BaseDto
    {
        public string Title { get; set; } = string.Empty;
        public string MetaTag { get; set; } = string.Empty;
        public string MetaDescription { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;

    }
    public class CategoryForShopDto : BaseDto
    {
        public string Title { get; set; } = string.Empty;
        public string MetaTag { get; set; } = string.Empty;
        public string MetaDescription { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public List<PostCategoryDto>? Posts { get; set; }

    }
    public class PostCategoryDto : BaseDto
    {
        public string? UserId { get; set; }
        public string? UserFullName { get; set; }
        //  public string CategoryTitle { get; set; }       
        public long CategoryId { get; set; }
        public string? Title { get; set; }
        public string? ImageName { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public int Visit { get; set; }

    }
}
