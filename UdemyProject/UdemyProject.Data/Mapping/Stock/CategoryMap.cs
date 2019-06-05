using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UdemyProject.Entities.Stock;

namespace UdemyProject.Data.Mapping.Stock
{
    public class CategoryMap : IEntityTypeConfiguration <Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category").HasKey(k => k._id_category);
            builder.Property(p => p.name).HasMaxLength(50);
            builder.Property(p => p.description).HasMaxLength(256);
        }
    }
}
