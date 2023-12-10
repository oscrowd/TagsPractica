using Microsoft.EntityFrameworkCore;
using TagsPractica.DAL.Models;


namespace TagsPractica.DAL
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Post> Posts  { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostsTags { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //optionsBuilder.UseSqlite("Filename=blog.db");


        //}
    }
}
//https://professorweb.ru/my/entity-framework/6/level2/2_2.php?ysclid=lpk3pd88dq77284330
//https://metanit.com/sharp/efcore/3.1.php?ysclid=lpk3p14y1o495881828
//https://yandex.ru/search/?text=c%23+mvc+entity+%D1%81%D0%B2%D1%8F%D0%B7%D0%B0%D0%BD%D0%BD%D1%8B%D0%B5&lr=36&clid=2411725