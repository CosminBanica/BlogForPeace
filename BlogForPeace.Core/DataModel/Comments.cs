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
        public int Upvotes { get; set; } = 0;
        public int Downvotes { get; set; } = 0;
    }
}
