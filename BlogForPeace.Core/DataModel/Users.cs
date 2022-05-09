using BlogForPeace.Core.SeedWork;

namespace BlogForPeace.Core.DataModel
{
    public class Users : Entity, IAggregateRoot
    {
        public Users(string identityId, string email, string name, string address)
        {
            IdentityId = identityId;
            Email = email;
            Name = name;
            Address = address;
        }

        public string IdentityId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<Comments> Comments { get; set; } = new List<Comments>();
        public ICollection<Comments> Upvoted { get; set; } = new List<Comments>();
        public ICollection<Comments> Downvoted { get; set; } = new List<Comments>();
    }
}
