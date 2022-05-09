using BlogForPeace.Api.Web;
using BlogForPeace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogForPeace.Api.Features.Blogposts.ViewBlogpost
{
    public class ViewBlogpostQueryHandler : IViewBlogpostQueryHandler
    {
        private readonly BlogForPeaceContext dbContext;

        public ViewBlogpostQueryHandler(BlogForPeaceContext blogForPeaceContext)
        {
            dbContext = blogForPeaceContext;
        }

        public async Task<BlogpostsWithCommentsDto> HandleAsync(int blogpostId, CancellationToken cancellationToken)
        {
            var blogpost = await dbContext.Blogposts
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == blogpostId, cancellationToken);

            if (blogpost == null)
            {
                throw new ApiException(System.Net.HttpStatusCode.NotFound, $"Blogpost with id {blogpostId} was not found.");
            }

            var blogpostWithComments = new BlogpostsWithCommentsDto(blogpost.Id, blogpost.Title, blogpost.Text, blogpost.Location, blogpost.Tags);

            var comments = await dbContext.Comments
                .AsNoTracking()
                .Include(x => x.Author)
                .Where(x => x.BlogpostId == blogpost.Id)
                .ToListAsync(cancellationToken);

            blogpostWithComments.Comments = comments;

            return blogpostWithComments;
        }
    }
}
