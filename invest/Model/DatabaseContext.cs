using Microsoft.EntityFrameworkCore;

namespace invest.Model
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<History> History { get; set; }
    }
}