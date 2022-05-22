using BlogForPeace.Api.Features.Profiles.RegisterProfile;
using BlogForPeace.Core.Domain.Blogpost;

namespace BlogForPeace.Api.Features.Profiles.EditProfile
{
    public record EditProfileCommand : RegisterProfileCommand
    {
        public EditProfileCommand(string email, string name, string address, IEnumerable<TagDto> tags) : base(email, name, address)
        {
            Tags = tags;
        }

        public IEnumerable<TagDto> Tags { get; init; } = new List<TagDto>();
    }
}
