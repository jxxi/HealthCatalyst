using Microsoft.EntityFrameworkCore;

namespace HealthCatalyst.Model
{
    public class PeopleContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=People.db");
        }
    }
}
