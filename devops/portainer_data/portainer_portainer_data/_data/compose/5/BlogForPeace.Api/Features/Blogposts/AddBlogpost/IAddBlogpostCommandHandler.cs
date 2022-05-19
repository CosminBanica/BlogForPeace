using BlogForPeace.Core.Domain;

namespace BlogForPeace.Api.Features.Blogposts.AddBlogpost
{
    public interface IAddBlogpostCommandHandler
    {
        public Task HandleAsync(AddBlogpostCommand command, CancellationToken cancellationToken);
    }
}
