using BlogForPeace.Api.Features.Blogposts.AddBlogpost;
using BlogForPeace.Core.Domain.Blogpost;

namespace BlogForPeace.Api.Features.Blogposts.EditBlogpost
{
    public record EditBlogpostCommand : AddBlogpostCommand
    {
        public EditBlogpostCommand(int id, string title, string text, string location, IEnumerable<TagDto> tags) : base(title, text, location, tags)
        {
            Id = id;
        }

        public int Id { get; init; }
    }
}
