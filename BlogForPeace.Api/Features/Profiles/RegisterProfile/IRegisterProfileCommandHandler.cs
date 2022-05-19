namespace BlogForPeace.Api.Features.Profiles.RegisterProfile
{
    public interface IRegisterProfileCommandHandler
    {
        public Task HandleAsync(string identityId, CancellationToken cancellationToken);
    }
}
