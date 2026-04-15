using System.Collections.Generic;
using MistAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MistAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Publishers> Publishers { get; set; }
        public DbSet<GamesInApp> GamesInApp { get; set; }
        public DbSet<UserOwnedGames> UserOwnedGames { get; set; }
        public DbSet<PaymentMethods> PaymentMethods { get; set; }
    }
}
