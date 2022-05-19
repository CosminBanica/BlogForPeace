namespace BlogForPeace.Api.Features.Blogposts.ViewAllBlogposts
{
    public interface IViewAllBlogpostsQueryHandler
    {
        public Task<IEnumerable<BlogpostDto>> HandleAsync(CancellationToken cancellationToken);
    }
}
