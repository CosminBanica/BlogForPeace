namespace BlogForPeace.Api.Features.Profiles.ViewProfile
{
    public interface IViewProfileQueryHandler
    {
        public Task<ProfileDto> HandleAsync(string identityId, CancellationToken cancellationToken);
    }
}
