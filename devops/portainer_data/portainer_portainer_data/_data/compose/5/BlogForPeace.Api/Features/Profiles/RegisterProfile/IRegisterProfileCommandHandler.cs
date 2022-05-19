namespace BlogForPeace.Api.Features.Profiles.RegisterProfile
{
    public interface IRegisterProfileCommandHandler
    {
        public Task HandleAsync(RegisterProfileCommand command, string identityId, CancellationToken cancellationToken);
    }
}
