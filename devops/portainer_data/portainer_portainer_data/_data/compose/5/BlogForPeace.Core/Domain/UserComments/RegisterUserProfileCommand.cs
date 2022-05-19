using BlogForPeace.Core.SeedWork;

namespace BlogForPeace.Core.Domain.UserComments
{
    public record RegisterUserProfileCommand : ICreateAggregateCommand
    {
        public RegisterUserProfileCommand(string identityId, string email, string name, string address)
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
    }
}
