using Blog.Domain.CommentAgg;
using Common.Query;

namespace Blog.Query.Comment.DTOs
{
    public class CommentDto : BaseDto
    {

        public string UserId { get; set; } = string.Empty;
        public long PostId { get; set; }
        public string Text { get; set; } = string.Empty;
        public CommentStatus Status { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
