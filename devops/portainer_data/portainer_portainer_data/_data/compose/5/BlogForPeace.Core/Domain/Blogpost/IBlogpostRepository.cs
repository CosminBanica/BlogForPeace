using BlogForPeace.Core.DataModel;
using BlogForPeace.Core.SeedWork;

namespace BlogForPeace.Core.Domain.Blogpost
{
    public interface IBlogpostRepository : IRepositoryOfAggregate<Blogposts, InsertBlogpostInBlogCommand>
    {
        public Task DeleteBlogpostAsync(Blogposts blogpost, CancellationToken cancellationToken);
    }
}
