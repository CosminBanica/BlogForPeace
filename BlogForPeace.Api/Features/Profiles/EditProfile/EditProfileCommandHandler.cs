using BlogForPeace.Core.Domain.UserComments;
using System.Net;
using BlogForPeace.Api.Web;
using MediatR;

namespace BlogForPeace.Api.Features.Profiles.EditProfile
{
    public class EditProfileCommandHandler : IEditProfileCommandHandler 
    {
        private readonly IUserCommentsRepository userCommentsRepository;
        private readonly IMediator mediator;

        public EditProfileCommandHandler(IUserCommentsRepository _userCommentsRepository, IMediator _mediator)
        {
            userCommentsRepository = _userCommentsRepository;
            this.mediator = _mediator;
        }

        public async Task HandleAsync(EditProfileCommand command, string identityId, CancellationToken cancellationToken)
        {
            var user = await userCommentsRepository.GetByIdentityAsync(identityId, cancellationToken) as UsersCommentsDomain;

            if (user == null)
            {
                throw new ApiException(HttpStatusCode.Unauthorized, $"User with identity {identityId} does not have a registered profile");
            }

            user.RemoveTags();
            foreach (var tag in command.Tags)
            {
                var userAddedTagEvent = user.AddTag(tag.Name, tag.Description);
            }

            user.UpdateProfile(command.Email, command.Name, command.Address);

            await userCommentsRepository.SaveAsync(cancellationToken);
        }
    }
}
