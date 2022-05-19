using BlogForPeace.Api.Web;
using BlogForPeace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogForPeace.Api.Features.Profiles.ViewProfile
{
    public class ViewProfileQueryHandler : IViewProfileQueryHandler
    {
        private readonly BlogForPeaceContext dbContext;

        public ViewProfileQueryHandler(BlogForPeaceContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<ProfileDto> HandleAsync(string identityId, CancellationToken cancellationToken)
        {
            var userProfile = await dbContext.Users
                .Where(x => x.IdentityId == identityId)
                .Select(x => new ProfileDto(x.Email, x.Name, x.Address, x.SubscribedTags))
                .FirstOrDefaultAsync(cancellationToken);

            if (userProfile == null)
            {
                throw new ApiException(System.Net.HttpStatusCode.Unauthorized, "This user does not have a registered profile!");
            }

            return userProfile;
        }
    }
}
