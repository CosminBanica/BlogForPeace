using BlogForPeace.Core.DataModel;

namespace BlogForPeace.Api.Features.Profiles.ViewProfile
{
    public record ProfileDto
    {
        public ProfileDto(string email, string name, string address, IEnumerable<Tags> tags)
        {
            Email = email;
            Name = name;
            Address = address;
            SubscribedTags = tags;
        }
        public string Email { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }
        public IEnumerable<Tags> SubscribedTags { get; init; }
    }
}
