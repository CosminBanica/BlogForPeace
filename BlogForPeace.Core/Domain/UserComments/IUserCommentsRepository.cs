using BlogForPeace.Core.DataModel;
using BlogForPeace.Core.SeedWork;

namespace BlogForPeace.Core.Domain.UserComments
{
    public interface IUserCommentsRepository : IRepositoryOfAggregate<Users, RegisterUserProfileCommand>
    {
        public Task<DomainOfAggregate<Users>?> GetByIdentityAsync(string identityId, CancellationToken cancellationToken);
    }
}
