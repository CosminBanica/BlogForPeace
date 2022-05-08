using MediatR;

namespace BlogForPeace.Core.Domain.UserComments
{
    public record UserDownvotedCommentEvent : INotification
    {
        public int CommentId { get; private set; }
        public UserDownvotedCommentEvent(int commentId)
        {
            CommentId = commentId;
        }
    }
}
