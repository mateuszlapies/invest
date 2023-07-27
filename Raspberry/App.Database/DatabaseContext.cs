﻿using Microsoft.EntityFrameworkCore;
using Raspberry.App.Database.Model;

namespace Raspberry.App.Database
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=App.db");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasIndex(i => i.Name)
                .IsUnique();

            modelBuilder.Entity<Item>()
                .Property(i => i.Created)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Price>()
                .Property(p => p.Created)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Order>()
                .Property(o => o.Created)
                .HasDefaultValueSql("getdate()");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}