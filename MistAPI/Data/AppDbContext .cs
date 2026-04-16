// FILE : AppDbContext.cs
// PROJECT : SENG2020 - MistApp
// PROGRAMMER : Zemmat Hagos, Will Jessel, Eric Moutoux
// FIRST VERSION : 2026-3-10
// DESCRIPTION :
// Where the database context is stored

using Microsoft.EntityFrameworkCore;
using MistAPI.Models.Entities;

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

            modelBuilder.Entity<GameInApp>()
                .HasOne(g => g.Publisher)
                .WithMany(p => p.GamesInApp)
                .HasForeignKey(g => g.PublisherID);
        }
    }
}
