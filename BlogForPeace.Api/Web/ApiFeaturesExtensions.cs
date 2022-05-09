using BlogForPeace.Api.Features.Blogposts;
//using BlogForPeace.Api.Features.Metrics;
//using BlogForPeace.Api.Features.Profile;
//using BlogForPeace.Api.Features.Comments;

namespace BlogForPeace.Api.Web
{
    public static class ApiFeaturesExtensions
    {
        public static void AddApiFeaturesHandlers(this IServiceCollection services)
        {
            // Add Blogpost Handlers
            services.AddBlogpostsHandlers();

            //// Add Metrics Handlers
            //services.AddMetricsHandlers();

            //// Add Profile Handlers
            //services.AddProfilesHandlers();

            //// Add Comments Handlers
            //services.AddCommentsHandlers();
        }
    }
}
