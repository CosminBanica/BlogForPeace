namespace BlogForPeace.Api.Features.Comment.LikeComment
{
    public interface ILikeCommentCommandHandler
    {
        public Task HandleAsync(int commentId, string identityId, CancellationToken cancellationToken);
    }
}
