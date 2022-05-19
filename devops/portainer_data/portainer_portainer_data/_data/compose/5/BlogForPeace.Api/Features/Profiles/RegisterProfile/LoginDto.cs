namespace BlogForPeace.Api.Features.Profiles.RegisterProfile
{
    public record LoginDto
    {
        public LoginDto(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; init; }
        public string Password { get; init; }
    }
}
