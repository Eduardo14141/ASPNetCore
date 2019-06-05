using Microsoft.EntityFrameworkCore;
using UdemyProject.Data.Mapping.Sales;
using UdemyProject.Data.Mapping.Stock;
using UdemyProject.Data.Mapping.Users;
using UdemyProject.Entities.Sales;
using UdemyProject.Entities.Stock;
using UdemyProject.Entities.Users;

namespace UdemyProject.Data
{
    public class DBSystemContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Rol> Rols{ get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Person> People { get; set; }
        public DBSystemContext(DbContextOptions<DBSystemContext> options) : base(options) { }

        //Permite mapear las entidades con la base de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new ArtcicleMap());
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new PersonMap());
        }
    }
}
