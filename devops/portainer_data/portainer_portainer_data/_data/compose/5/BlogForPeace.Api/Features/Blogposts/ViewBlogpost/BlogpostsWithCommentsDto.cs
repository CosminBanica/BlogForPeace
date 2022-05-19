using BlogForPeace.Api.Features.Blogposts.ViewAllBlogposts;
using BlogForPeace.Core.DataModel;

namespace BlogForPeace.Api.Features.Blogposts.ViewBlogpost
{
    public record BlogpostsWithCommentsDto : BlogpostDto
    {
        public BlogpostsWithCommentsDto(int id, string title, string text, string location, IEnumerable<Tags> tags) 
            : base(id, title, text, location, tags)
        {
        }

        public ICollection<Comments> Comments { get; set; } = new List<Comments>();
    }
}
