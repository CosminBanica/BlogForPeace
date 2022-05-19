using BlogForPeace.Api.Web;
using BlogForPeace.Core.Domain.Blogpost;
using BlogForPeace.Core.Domain.UserComments;
using MediatR;
using System.Net;

namespace BlogForPeace.Api.Features.Comment.AddComment
{
    public class AddCommentCommandHandler : IAddCommentCommandHandler
    {
        private readonly IBlogpostRepository blogpostRepository;
        private readonly IUserCommentsRepository userCommentsRepository;
        private readonly IMediator mediator;

        public AddCommentCommandHandler(IBlogpostRepository _blogpostRepository, IUserCommentsRepository _userCommentsRepository, IMediator _mediator)
        {
            this.blogpostRepository = _blogpostRepository;
            this.userCommentsRepository = _userCommentsRepository;
            this.mediator = _mediator;
        }

        public async Task HandleAsync(AddCommentCommand command, string identityId, CancellationToken cancellationToken)
        {
            var blogpost = await blogpostRepository.GetAsync(command.BlogpostId, cancellationToken) as BlogpostDomain;

            if (blogpost == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, $"Blogpost with id {command.BlogpostId} not found!");
            }

            var user = await userCommentsRepository.GetByIdentityAsync(identityId, cancellationToken) as UsersCommentsDomain;

            if (user == null)
            {
                throw new ApiException(HttpStatusCode.Unauthorized, $"User with identity {identityId} does not have a registered profile");
            }
            
            var userCommentedOnPostEvent = user.CommentOnPost(command.BlogpostId, command.Message);
            await mediator.Publish(userCommentedOnPostEvent, cancellationToken);

            await userCommentsRepository.SaveAsync(cancellationToken);
        }

    }
}
