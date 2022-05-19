using BlogForPeace.Core.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogForPeace.Infrastructure.Data.EntityConfigurations
{
    internal class CommentsConfiguration : EntityConfiguration<Comments>
    {
        public override void Configure(EntityTypeBuilder<Comments> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.BlogpostId)
                .IsRequired();

            builder
                .Property(x => x.UserId)
                .IsRequired();

            builder
                .Property(x => x.Message)
                .IsRequired();

            builder
                .HasOne<Users>(x => x.Author)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .HasOne<Blogposts>(x => x.Blogpost)
                .WithMany()
                .HasForeignKey(x => x.BlogpostId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .HasMany(x => x.Upvotes)
                .WithMany(x => x.Upvoted);

            builder
                .HasMany(x => x.Downvotes)
                .WithMany(x => x.Downvoted);


            base.Configure(builder);
        }
    }
}
