using BlogForPeace.Core.Domain.UserComments;
using BlogForPeace.Core.Domain.Blogpost;
using BlogForPeace.Infrastructure.Data;
using BlogForPeace.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogForPeace.Api.Infrastructure
{
    public static partial class DataAccessExtensions
    {
        public static void AddBlogForPeaceDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<BlogForPeaceContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("BlogForPeaceDb")));
        }

        public static void AddBlogForPeaceAggregateRepositories(this IServiceCollection services)
        {
            services.AddTransient<IBlogpostRepository, BlogpostsRepository>();
            services.AddTransient<IUserCommentsRepository, UserCommentsRepository>();
        }
    }
}
