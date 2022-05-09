using BlogForPeace.Core.Domain.Blogpost;

namespace BlogForPeace.Api.Features.Blogposts.AddBlogpost
{
    public record AddBlogpostCommand : InsertBlogpostInBlogCommand
    {
        public AddBlogpostCommand(string title, string text, string location, IEnumerable<TagDto> tags) : base(title, text, location, tags)
        {
        }
    }
}
