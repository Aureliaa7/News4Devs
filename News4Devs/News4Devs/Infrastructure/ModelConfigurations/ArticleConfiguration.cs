using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News4Devs.Shared.Entities;

namespace News4Devs.Infrastructure.ModelConfigurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.Title);

            builder.Property(a => a.Url)
              .IsRequired();

            builder.Property(a => a.Description)
             .IsRequired();

            builder.Property(a => a.PublishedAt)
             .IsRequired();

            builder.Property(a => a.PublishedAt)
             .IsRequired();

            builder.Property(a => a.Content)
             .IsRequired();
        }
    }
}
