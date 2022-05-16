using BlogForPeace.Core.Domain.Blogpost;
using BlogForPeace.Api.Web;
using MediatR;
using System.Net;

namespace BlogForPeace.Api.Features.Blogposts.EditBlogpost
{
    public class EditBlogpostCommandHandler : IEditBlogpostCommandHandler
    {
        private readonly IBlogpostRepository blogpostRepository;

        public EditBlogpostCommandHandler(IBlogpostRepository _blogpostRepository)
        {
            this.blogpostRepository = _blogpostRepository;
        }

        public async Task HandleAsync(EditBlogpostCommand command, CancellationToken cancellationToken)
        {
            var blogpost = await blogpostRepository.GetAsync(command.Id, cancellationToken) as BlogpostDomain;

            if (blogpost == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, $"Blogpost with id {command.Id} not found!");
            }

            foreach (var tag in command.Tags)
            {
                blogpost.AddTag(tag.Name, tag.Description);
            }

            blogpost.UpdateBlogpost(command.Title, command.Text, command.Location);

            await blogpostRepository.SaveAsync(cancellationToken);
        }
    }
}
