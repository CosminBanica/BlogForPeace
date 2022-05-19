using BlogForPeace.Core.DataModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogForPeace.Infrastructure.Data.EntityConfigurations
{
    internal class TagConfiguration : EntityConfiguration<Tags>
    {
        public override void Configure(EntityTypeBuilder<Tags> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired();

            builder
                .Property(x => x.Description)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
