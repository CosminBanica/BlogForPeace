using BlogForPeace.Core.Domain.UserComments;
using BlogForPeace.Core.Domain.Blogpost;
using BlogForPeace.Infrastructure.Data;
using BlogForPeace.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using BlogForPeace.Api.Features.Profiles.RegisterProfile;
using Microsoft.Extensions.Options;

namespace BlogForPeace.Api.Infrastructure
{
    public static partial class DataAccessExtensions
    {
        public static void AddBlogForPeaceDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<BlogForPeaceContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("BlogForPeaceDb")));

            builder.Services.Configure<JWTConfig>(builder.Configuration.GetSection("JWTConfig"));
            var serviceProvider = builder.Services.BuildServiceProvider();
            var opt = serviceProvider.GetRequiredService<IOptions<JWTConfig>>().Value;
        }

        public static void AddBlogForPeaceAggregateRepositories(this IServiceCollection services)
        {
            services.AddTransient<IBlogpostRepository, BlogpostsRepository>();
            services.AddTransient<IUserCommentsRepository, UserCommentsRepository>();
        }
    }
}
