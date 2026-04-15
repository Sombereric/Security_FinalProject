using System.Collections.Generic;
using MistAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MistAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<GameInApp> GamesInApp { get; set; }
        public DbSet<UserOwnedGame> UserOwnedGames { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserOwnedGame>()
                .HasKey(uog => new { uog.GameID, uog.UserID });
        }
    }
}
