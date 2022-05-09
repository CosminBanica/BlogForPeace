using BlogForPeace.Core.DataModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogForPeace.Infrastructure.Data.EntityConfigurations
{
    internal class BlogpostsConfiguration : EntityConfiguration<Blogposts>
    {
        public override void Configure(EntityTypeBuilder<Blogposts> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Title)
                .IsRequired();

            builder
                .Property(x => x.Text)
                .IsRequired();

            builder
                .Property(x => x.Location)
                .IsRequired();

            builder
                .OwnsMany(x => x.Tags);

            base.Configure(builder);
        }
    }
}
