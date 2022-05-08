namespace BlogForPeace.Core.Domain.UserComments
{
    public class CommentNotFoundException : Exception
    {
        public CommentNotFoundException(int commentId) : base($"Comment {commentId} not found in user's comments!")
        {
        }
    }
}
