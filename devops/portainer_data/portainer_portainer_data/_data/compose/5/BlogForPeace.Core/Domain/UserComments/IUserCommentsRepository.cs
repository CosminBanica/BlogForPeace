using BlogForPeace.Core.DataModel;
using BlogForPeace.Core.SeedWork;

namespace BlogForPeace.Core.Domain.UserComments
{
    public interface IUserCommentsRepository : IRepositoryOfAggregate<Users, RegisterUserProfileCommand>
    {
        public Task<DomainOfAggregate<Users>?> GetByIdentityAsync(string identityId, CancellationToken cancellationToken);
        public DomainOfAggregate<Users>? GetByIdentity(string identityId);
        public Task<DomainOfAggregate<Users>?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
