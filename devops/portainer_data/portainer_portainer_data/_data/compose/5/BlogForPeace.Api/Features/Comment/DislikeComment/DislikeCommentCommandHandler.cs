using BlogForPeace.Api.Web;
using BlogForPeace.Core.Domain.UserComments;
using MediatR;
using System.Net;

namespace BlogForPeace.Api.Features.Comment.DislikeComment
{
    public class DislikeCommentCommandHandler : IDislikeCommentCommandHandler
    {
        private readonly IUserCommentsRepository userCommentsRepository;
        private readonly IMediator mediator;

        public DislikeCommentCommandHandler(IUserCommentsRepository _userCommentsRepository, IMediator _mediator)
        {
            this.userCommentsRepository = _userCommentsRepository;
            this.mediator = _mediator;
        }

        public async Task HandleAsync(int commentId, string identityId, CancellationToken cancellationToken)
        {
            var user = await userCommentsRepository.GetByIdentityAsync(identityId, cancellationToken) as UsersCommentsDomain;

            if (user == null)
            {
                throw new ApiException(HttpStatusCode.Unauthorized, $"User with identity {identityId} does not have a registered profile");
            }

            var userDownvotedCommentEvent = user.DownvoteComment(commentId);
            await mediator.Publish(userDownvotedCommentEvent, cancellationToken);

            await userCommentsRepository.SaveAsync(cancellationToken);
        }
    }
}
