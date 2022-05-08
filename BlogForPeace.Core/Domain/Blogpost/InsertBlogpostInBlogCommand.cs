using BlogForPeace.Core.SeedWork;

namespace BlogForPeace.Core.Domain.Blogpost
{
    public record InsertBlogpostInBlogCommand : ICreateAggregateCommand
    {
        public string Title { get; init; }
        public string Text { get; init; }   
        public string Location { get; init; }
        public virtual IEnumerable<TagDto> Tags { get; init; } = new List<TagDto>();

        public InsertBlogpostInBlogCommand(string title, string text, string location, IEnumerable<TagDto> tags)
        {
            Title = title;
            Text = text;
            Location = location;
            Tags = tags;
        }
    }

    public record TagDto (string Name, string Description);
}
