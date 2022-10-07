using KeyboardMenu;
using Microsoft.EntityFrameworkCore;

namespace Menu
{

    public class ApplicationContext : DbContext
    {

        public DbSet<Player> Players { get; set; } = null;

        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=helloapp.db");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Player>().HasNoKey().
        //}


    }

}

