using BlogForPeace.Core.SeedWork;

namespace BlogForPeace.Core.DataModel
{
    public class Blogposts : Entity, IAggregateRoot
    {
        public Blogposts(string title, string text, string location)
        {
            Title = title;
            Text = text;
            Location = location;
        }

        public string Title { get; set; }
        public string Text { get; set; }
        public string Location { get; set; }
        public ICollection<Comments> Comments { get; set; } = new List<Comments>();
        public virtual ICollection<Tags> Tags { get; set; } = new List<Tags>();
    }
}
