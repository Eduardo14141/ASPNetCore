using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UdemyProject.Entities.Stock;

namespace UdemyProject.Data.Mapping.Stock
{
    public class ArtcicleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Article").HasKey(k => k._id_article);
        }
    }
}
