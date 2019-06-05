using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UdemyProject.Entities.Users;

namespace UdemyProject.Data.Mapping.Users
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("_User").HasKey(k => k._id_user);
        }
    }
}
