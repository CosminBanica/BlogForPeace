namespace BlogForPeace.Api.Features.Comment.AddComment
{
    public interface IAddCommentCommandHandler
    {
        public Task HandleAsync(AddCommentCommand command, string identityId, CancellationToken cancellationToken);
    }
}
