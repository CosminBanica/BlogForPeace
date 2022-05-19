namespace BlogForPeace.Api.Features.Comment.AddComment
{
    public record AddCommentCommand
    {
        public AddCommentCommand(int blogpostId, int userId, string message)
        {
            BlogpostId = blogpostId;
            UserId = userId;
            Message = message;
        }

        public int BlogpostId { get; init; }
        public int UserId { get; init; }
        public string Message { get; init; }
    }
}
