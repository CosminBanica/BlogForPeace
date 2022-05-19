using BlogForPeace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogForPeace.Api.Features.Blogposts.ViewAllBlogposts
{
    public class ViewAllBlogpostsQueryHandler : IViewAllBlogpostsQueryHandler
    {
        private readonly BlogForPeaceContext dbContext;

        public ViewAllBlogpostsQueryHandler(BlogForPeaceContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task<IEnumerable<BlogpostDto>> HandleAsync(CancellationToken cancellationToken)
        {
            var blogposts = await dbContext.Blogposts
                .AsNoTracking()
                .Select(x => new BlogpostDto(x.Id, x.Title, x.Text, x.Location, x.Tags))
                .ToListAsync(cancellationToken);

            return blogposts;
        }
    }
}
