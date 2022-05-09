using BlogForPeace.Core.SeedWork;

namespace BlogForPeace.Core.DataModel
{
    public class Comments : Entity
    {
        public Comments(int blogpostId, int userId, string message)
        {
            BlogpostId = blogpostId;
            UserId = userId;
            Message = message;
        }

        public int BlogpostId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public ICollection<Users> Upvotes { get; set; } = new List<Users>();
        public ICollection<Users> Downvotes { get; set; } = new List<Users>();
        public virtual Users Author { get; set; } = null!;
        public virtual Blogposts Blogpost { get; set; } = null!;
    }
}
