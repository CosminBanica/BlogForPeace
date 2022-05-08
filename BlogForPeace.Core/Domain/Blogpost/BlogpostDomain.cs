using BlogForPeace.Core.DataModel;
using BlogForPeace.Core.SeedWork;

namespace BlogForPeace.Core.Domain.Blogpost
{
    public class BlogpostDomain : DomainOfAggregate<Blogposts>
    {
        public BlogpostDomain(Blogposts aggregate) : base(aggregate)
        {
        }

        public void UpdateBlogpost(string title, string text, string location, ICollection<Tags> tags)
        {
            aggregate.Title = title;
            aggregate.Text = text;
            aggregate.Location = location;
            aggregate.Tags = tags;
        }
    }
}
