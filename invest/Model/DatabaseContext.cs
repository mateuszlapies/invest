using Microsoft.EntityFrameworkCore;

namespace invest.Model
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Cookie> Cookies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Item>().HasData(new Item() { ItemId = 1, Name = "Antwerp 2022 Legends Sticker Capsule", Hash = "Antwerp%202022%20Legends%20Sticker%20Capsule", BuyPrice = 0.98, BuyAmount = 30 });
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