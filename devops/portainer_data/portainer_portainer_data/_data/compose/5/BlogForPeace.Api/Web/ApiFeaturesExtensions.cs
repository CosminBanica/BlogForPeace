using BlogForPeace.Api.Features.Blogposts;
using BlogForPeace.Api.Features.Profiles;
using BlogForPeace.Api.Features.Comment;

namespace BlogForPeace.Api.Web
{
    public static class ApiFeaturesExtensions
    {
        public static void AddApiFeaturesHandlers(this IServiceCollection services)
        {
            // Add Blogpost Handlers
            services.AddBlogpostsHandlers();

            // Add Profile Handlers
            services.AddProfilesHandlers();

            // Add Comments Handlers
            services.AddCommentsHandlers();
        }
    }
}
