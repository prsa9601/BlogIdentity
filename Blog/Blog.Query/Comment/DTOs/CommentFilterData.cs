using Blog.Domain.CommentAgg;
using Common.Query;

namespace Blog.Query.Comment.DTOs;

public class CommentFilterData : BaseDto
{
    public string? UserId { get; set; }
    public long PostId { get; set; }
    public string? Text { get; set; }
    public CommentStatus Status { get; set; }
    public DateTime UpdateDate { get; set; }
}
public class CommentFilterDataProduct : BaseDto
{
    public string? UserId { get; set; }
    public string? UserAvatar { get; set; } 
    public string? UserName { get; set; }
    public long PostId { get; set; }
    public string? Text { get; set; }
    public CommentStatus Status { get; set; }
    public DateTime UpdateDate { get; set; }
}