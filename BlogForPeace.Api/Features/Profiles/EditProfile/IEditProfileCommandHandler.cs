namespace BlogForPeace.Api.Features.Profiles.EditProfile
{
    public interface IEditProfileCommandHandler
    {
        public Task HandleAsync(EditProfileCommand command, string identityId, CancellationToken cancellationToken);
    }
}
