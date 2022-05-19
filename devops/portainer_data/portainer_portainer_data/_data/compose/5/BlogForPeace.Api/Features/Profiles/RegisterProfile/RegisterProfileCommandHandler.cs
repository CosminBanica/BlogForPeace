using BlogForPeace.Core.Domain.UserComments;

namespace BlogForPeace.Api.Features.Profiles.RegisterProfile
{
    public class RegisterProfileCommandHandler : IRegisterProfileCommandHandler
    {
        private readonly IUserCommentsRepository usersCommentsRepository;

        public RegisterProfileCommandHandler(IUserCommentsRepository _userCommentsRepository)
        {
            usersCommentsRepository = _userCommentsRepository;
        }

        public Task HandleAsync(RegisterProfileCommand command, string identityId, CancellationToken cancellationToken)
            => usersCommentsRepository.AddAsync(
                new RegisterUserProfileCommand(identityId, command.Email, command.Name, command.Address),
                cancellationToken);
    }
}
