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
                .Property(x => x.BlogpostId)
                .IsRequired();

            builder
                .Property(x => x.UserId)
                .IsRequired();

            builder
                .Property(x => x.Message)
                .IsRequired();

            builder
                .HasOne<Blogposts>()
                .WithMany()
                .HasForeignKey(x => x.BlogpostId)
                .OnDelete(DeleteBehavior.ClientSetNull);


            base.Configure(builder);
        }
    }
}
