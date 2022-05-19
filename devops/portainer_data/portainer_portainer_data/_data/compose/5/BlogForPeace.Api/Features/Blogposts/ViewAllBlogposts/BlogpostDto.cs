using BlogForPeace.Core.DataModel;

namespace BlogForPeace.Api.Features.Blogposts.ViewAllBlogposts
{
    public record BlogpostDto
    {
        public BlogpostDto(int id, string title, string text, string location, IEnumerable<Tags> tags)
        {
            Id = id;
            Title = title;
            Text = text;
            Location = location;
            Tags = tags;
        }

        public int Id { get; init; }
        public string Title { get; init; }
        public string Text { get; init; }
        public string Location { get; init; }
        public IEnumerable<Tags> Tags { get; init; }
    }
}
