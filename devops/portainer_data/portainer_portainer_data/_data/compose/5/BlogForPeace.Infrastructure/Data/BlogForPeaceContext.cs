using BlogForPeace.Core.DataModel;
using BlogForPeace.Infrastructure.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace BlogForPeace.Infrastructure.Data
{
    public class BlogForPeaceContext : DbContext
    {
        public BlogForPeaceContext(DbContextOptions<BlogForPeaceContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogpostsConfiguration).Assembly)
                        .ApplyConfigurationsFromAssembly(typeof(UsersConfiguration).Assembly)
                        .ApplyConfigurationsFromAssembly(typeof(CommentsConfiguration).Assembly)
                        .ApplyConfigurationsFromAssembly(typeof(Tags).Assembly);
        }

        public DbSet<Users> Users => Set<Users>();
        public DbSet<Blogposts> Blogposts => Set<Blogposts>();
        public DbSet<Comments> Comments => Set<Comments>();
        public DbSet<Tags> Tags => Set<Tags>();
    }
}
