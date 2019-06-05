using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UdemyProject.Entities.Sales;

namespace UdemyProject.Data.Mapping.Sales
{
    public class PersonMap : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person").HasKey(k => k._id_person);
        }
    }
}
