using BlogForPeace.Api.Web;
using BlogForPeace.Core.Domain.UserComments;
using MediatR;
using System.Net;

namespace BlogForPeace.Api.Features.Comment.LikeComment
{
    public class LikeCommentCommandHandler : ILikeCommentCommandHandler
    {
        private readonly IUserCommentsRepository userCommentsRepository;
        private readonly IMediator mediator;

        public LikeCommentCommandHandler(IUserCommentsRepository _userCommentsRepository, IMediator _mediator)
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

            var userUpvotedCommentEvent = user.UpvoteComment(commentId);
            await mediator.Publish(userUpvotedCommentEvent, cancellationToken);

            await userCommentsRepository.SaveAsync(cancellationToken);
        }
    }
}
