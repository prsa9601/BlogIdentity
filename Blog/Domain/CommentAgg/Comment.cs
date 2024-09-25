using Common.Domain;
using Common.Domain.Exceptions;

namespace Blog.Domain.CommentAgg
{
    public class Comment : AggregateRoot
    {
        public string UserId { get; set; }
        public long PostId { get; set; }
        public string Text { get; set; }
        public CommentStatus Status { get; set; }
        public DateTime UpdateDate { get; set; }

        public Comment(string userId, long postId, string text)
        {
            NullOrEmptyDomainDataException.CheckString(text, nameof(text));

            UserId = userId;
            PostId = postId;
            Text = text;
            Status = CommentStatus.Pending;
        }

        public void Edit(string text)
        {
            NullOrEmptyDomainDataException.CheckString(text, nameof(text));

            Text = text;
            UpdateDate = DateTime.Now;
        }

        public void ChangeStatus(CommentStatus status)
        {
            Status = status;
            UpdateDate = DateTime.Now;
        }
    }

    public enum CommentStatus
    {
        Pending,
        Accepted,
        Rejected
    }
}