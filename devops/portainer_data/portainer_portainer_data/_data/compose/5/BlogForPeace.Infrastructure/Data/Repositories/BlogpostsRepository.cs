using BlogForPeace.Core.DataModel;
using BlogForPeace.Core.Domain.Blogpost;
using BlogForPeace.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace BlogForPeace.Infrastructure.Data.Repositories
{
    public class BlogpostsRepository : IBlogpostRepository
    {
        private readonly BlogForPeaceContext context;

        public BlogpostsRepository(BlogForPeaceContext _context)
        {
            this.context = _context;
        }

        public async Task AddAsync(InsertBlogpostInBlogCommand command, CancellationToken cancellationToken)
        {
            var tags = command.Tags.Select(x => new Tags(x.Name, x.Description)).ToList();

            var blogpost = new Blogposts(command.Title, command.Text, command.Location);
            blogpost.Tags = tags;

            await context.Blogposts.AddAsync(blogpost, cancellationToken);
            await SaveAsync(cancellationToken);
        }

        public async Task<DomainOfAggregate<Blogposts>?> GetAsync(int aggregateId, CancellationToken cancellationToken)
        {
            var blogpost = await context.Blogposts
                .FirstOrDefaultAsync(x => x.Id == aggregateId, cancellationToken);

            if (blogpost == null)
            {
                return null;
            }

            return new BlogpostDomain(blogpost);
        }

        public async Task DeleteBlogpostAsync(Blogposts blogpost, CancellationToken cancellationToken)
        {
            context.Blogposts.Remove(blogpost);

            await SaveAsync(cancellationToken);
        }

        public Task SaveAsync(CancellationToken cancellationToken) => context.SaveChangesAsync(cancellationToken);
    }
}
