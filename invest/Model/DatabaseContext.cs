using invest.Model.NBP;
using invest.Model.Steam;
using Microsoft.EntityFrameworkCore;

namespace invest.Model
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserItem> UserItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public List<Daily> Dailies { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Cookie> Cookies { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<Rate> Rates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Item>().HasData(
                new Item() { ItemId = 1, Key = "A2LSC", Name = "Antwerp 2022 Legends Sticker Capsule", Hash = "Antwerp%202022%20Legends%20Sticker%20Capsule" },
                new Item() { ItemId = 2, Key = "OHC", Name = "Operation Hydra Case", Hash = "Operation%20Hydra%20Case" }
            );

            builder.Entity<Cookie>().HasData(new Cookie[]
            {
                 new Cookie() { CookieId = 1, Name = "steamLoginSecure", Value = "76561199480411558%7C%7C4ADF3CEAC29FEDE4F05761F7FB323DAFDE517DC2", Expires = DateTime.Now.ToUniversalTime().AddDays(6) },
                 new Cookie() { CookieId = 2, Name = "sessionid", Value = "f4aead9f9d5fb574d07c665c", Expires = DateTime.Now.ToUniversalTime().AddDays(6) }
            });

            builder.Entity<Item>()
                .HasMany(i => i.Points)
                .WithOne(p => p.Item)
                .HasForeignKey(p => p.ItemId);
        }
    }
}