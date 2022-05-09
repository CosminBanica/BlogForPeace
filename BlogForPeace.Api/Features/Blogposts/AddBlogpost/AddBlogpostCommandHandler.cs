using BlogForPeace.Core.Domain.Blogpost;

namespace BlogForPeace.Api.Features.Blogposts.AddBlogpost
{
    public class AddBlogpostCommandHandler : IAddBlogpostCommandHandler
    {
        private readonly IBlogpostRepository blogpostRepository;

        public AddBlogpostCommandHandler(IBlogpostRepository _blogpostRepository)
        {
            this.blogpostRepository = _blogpostRepository;
        }

        public Task HandleAsync(AddBlogpostCommand command, CancellationToken cancellationToken)
            => blogpostRepository.AddAsync(
                new InsertBlogpostInBlogCommand(command.Title, command.Text, command.Location, command.Tags),
                cancellationToken);
    }
}
