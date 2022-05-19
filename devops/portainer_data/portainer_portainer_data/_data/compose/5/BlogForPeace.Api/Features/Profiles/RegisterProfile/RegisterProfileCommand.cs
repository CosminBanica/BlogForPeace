namespace BlogForPeace.Api.Features.Profiles.RegisterProfile
{
    public record RegisterProfileCommand
    {
        public RegisterProfileCommand(string email, string name, string address)
        {
            Email = email;
            Name = name;
            Address = address;
        }

        public string Email { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }
    }
}
