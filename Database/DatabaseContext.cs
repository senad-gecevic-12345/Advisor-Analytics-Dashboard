using Microsoft.EntityFrameworkCore;
namespace fullstack_1.Database
{
    public class DatabaseContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("");
        }
        // public DbSet<>
    }
}
