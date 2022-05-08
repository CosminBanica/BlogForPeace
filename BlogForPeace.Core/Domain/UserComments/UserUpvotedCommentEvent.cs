using MediatR;

namespace BlogForPeace.Core.Domain.UserComments
{
    public record UserUpvotedCommentEvent : INotification
    {
        public int CommentId { get; private set; }
        public UserUpvotedCommentEvent(int commentId)
        {
            CommentId = commentId;
        }
    }
}
