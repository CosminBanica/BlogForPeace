using BlogForPeace.Core.DataModel;
using BlogForPeace.Core.Domain;
using BlogForPeace.Core.Domain.UserComments;
using BlogForPeace.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace BlogForPeace.Infrastructure.Data.Repositories
{
    public class UserCommentsRepository : IUserCommentsRepository
    {
        private readonly BlogForPeaceContext context;

        public UserCommentsRepository(BlogForPeaceContext _context)
        {
            this.context = _context;
        }

        public async Task AddAsync(RegisterUserProfileCommand command, CancellationToken cancellationToken)
        {
            var user = new Users(command.IdentityId, command.Email, command.Name, command.Address);

            await context.Users.AddAsync(user);
            await SaveAsync(cancellationToken);
        }

        public async Task<DomainOfAggregate<Users>?> GetAsync(int aggregateId, CancellationToken cancellationToken)
        {
            var entity = await context.Users
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == aggregateId, cancellationToken);

            if (entity == null)
            {
                return null;
            }

            return new UsersCommentsDomain(entity);
        }

        public async Task<DomainOfAggregate<Users>?> GetByIdentityAsync(string identityId, CancellationToken cancellationToken)
        {
            var entity = await context.Users
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.IdentityId == identityId, cancellationToken);

            if (entity == null)
            {
                return null;
            }

            return new UsersCommentsDomain(entity);
        }

        public Task SaveAsync(CancellationToken cancellationToken) => context.SaveChangesAsync(cancellationToken);
    }
}
