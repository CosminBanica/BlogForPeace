using BlogForPeace.Core.DataModel;
using BlogForPeace.Core.SeedWork;

namespace BlogForPeace.Core.Domain.UserComments
{
    public class UsersCommentsDomain : DomainOfAggregate<Users>
    {
        public UsersCommentsDomain(Users aggregate) : base(aggregate)
        {
        }

        public void UpdateProfile(string email, string name, string address)
        {
            aggregate.Email = email;
            aggregate.Name = name;
            aggregate.Address = address;
        }

        public UserCommentedOnPostEvent CommentOnPost(int blogpostId, string message)
        {
            aggregate.Comments.Add(new Comments(blogpostId, aggregate.Id, message));

            return new UserCommentedOnPostEvent(blogpostId);
        }

        public UserUpvotedCommentEvent UpvoteComment(int commentId)
        {
            var comment = aggregate.Comments.FirstOrDefault(c => c.Id == commentId);

            if (comment == null)
            {
                throw new CommentNotFoundException(commentId);
            }

            comment.Upvotes.Add(aggregate);

            return new UserUpvotedCommentEvent(commentId);
        }

        public UserDownvotedCommentEvent DownvoteComment(int commentId)
        {
            var comment = aggregate.Comments.FirstOrDefault(c => c.Id == commentId);

            if (comment == null)
            {
                throw new CommentNotFoundException(commentId);
            }

            comment.Downvotes.Add(aggregate);

            return new UserDownvotedCommentEvent(commentId);
        }
    }
}
