using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UdemyProject.Entities.Users;

namespace UdemyProject.Data.Mapping.Users
{
    public class RolMap : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("Rol").HasKey(k => k._id_rol);
        }
    }
}
