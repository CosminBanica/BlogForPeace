namespace BlogForPeace.Api.Features.Comment.DislikeComment
{
    public interface IDislikeCommentCommandHandler
    {
        public Task HandleAsync(int commentId, string identityId, CancellationToken cancellationToken);
    }
}
