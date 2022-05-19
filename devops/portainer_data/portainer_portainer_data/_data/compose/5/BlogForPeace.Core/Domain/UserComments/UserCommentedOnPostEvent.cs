using MediatR;

namespace BlogForPeace.Core.Domain.UserComments
{
    public record UserCommentedOnPostEvent : INotification
    {
        public int BlogpostId { get; private set; }
        public UserCommentedOnPostEvent(int blogpostId)
        {
            BlogpostId = blogpostId;
        }
    }
}
