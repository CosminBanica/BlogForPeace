namespace BlogForPeace.Api.Features.Blogposts.ViewBlogpost
{
    public interface IViewBlogpostQueryHandler
    {
        public Task<BlogpostsWithCommentsDto> HandleAsync(int blogpostId, CancellationToken cancellationToken);
    }
}
