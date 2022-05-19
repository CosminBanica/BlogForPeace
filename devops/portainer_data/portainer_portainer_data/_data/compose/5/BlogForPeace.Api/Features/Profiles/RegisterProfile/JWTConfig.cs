namespace BlogForPeace.Api.Features.Profiles.RegisterProfile
{
    public class JWTConfig
    {
        public JWTConfig(string key, string issuer, string audience)
        {
            Key = key;
            Issuer = issuer;
            Audience = audience;
        }

        public JWTConfig()
        {
            Key = "";
            Issuer = "";
            Audience = "";
        }

        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
