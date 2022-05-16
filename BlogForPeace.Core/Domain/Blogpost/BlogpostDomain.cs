using BlogForPeace.Core.DataModel;
using BlogForPeace.Core.SeedWork;

namespace BlogForPeace.Core.Domain.Blogpost
{
    public class BlogpostDomain : DomainOfAggregate<Blogposts>
    {
        public BlogpostDomain(Blogposts aggregate) : base(aggregate)
        {
        }

        public void UpdateBlogpost(string title, string text, string location)
        {
            aggregate.Title = title;
            aggregate.Text = text;
            aggregate.Location = location;
        }

        public void AddTag(string name, string description)
        {
            aggregate.Tags.Add(new Tags(name, description));
        }

        public void RemoveTags()
        {
            aggregate.Tags.Clear();
        }
    }
}
