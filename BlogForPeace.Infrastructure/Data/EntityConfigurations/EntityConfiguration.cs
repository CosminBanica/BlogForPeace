using BlogForPeace.Core.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogForPeace.Infrastructure.Data.EntityConfigurations
{
    internal abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder
                .Property(x => x.CreatedAt)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}
