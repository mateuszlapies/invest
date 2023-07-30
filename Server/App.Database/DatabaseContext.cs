using Microsoft.EntityFrameworkCore;
using Raspberry.App.Model.Database;

namespace App.Database
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("server=postgres;database=invest;user id=postgres;password=postgres");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Item>()
                .Property(i => i.Created)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Price>()
                .Property(i => i.Created)
                .HasDefaultValueSql("getdate()");
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Price> Prices { get; set; }
    }
}