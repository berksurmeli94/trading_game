using TG.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TG.Domain.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<SharePrice> SharePrices { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Transaction> TransActionHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                   .HasKey(s => s.ID);

            modelBuilder.Entity<User>()
                    .Property(s => s.TotalMoney)
                    .HasPrecision(18, 2);

            modelBuilder.Entity<Share>()
                    .HasKey(s => s.ID);

            modelBuilder.Entity<Share>()
                    .HasMany(x => x.SharePrices)
                    .WithOne(s => s.Share);

            modelBuilder.Entity<Share>()
                   .Property(s => s.TotalWorthOfShare)
                    .HasPrecision(18, 2);

            modelBuilder.Entity<Transaction>()
                     .HasKey(s => s.ID);

            modelBuilder.Entity<Portfolio>()
                     .HasKey(s => s.ID);

            modelBuilder.Entity<SharePrice>()
                     .HasKey(s => s.ID);

            modelBuilder.Entity<SharePrice>()
                    .HasOne(x => x.Share)
                    .WithMany(s => s.SharePrices);

            modelBuilder.Entity<SharePrice>()
                .Property(s => s.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Portfolio>()
                   .HasKey(s => s.ID);

            modelBuilder.Entity<Portfolio>()
                   .Property(s => s.TotalPrice)
                   .HasPrecision(18, 2);
        }
    }
}
