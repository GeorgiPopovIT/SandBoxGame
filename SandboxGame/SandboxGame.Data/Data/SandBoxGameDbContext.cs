using Microsoft.EntityFrameworkCore;
using SandboxGame.Data.Models;

namespace SandboxGame.Data.Data
{
    public class SandBoxGameDbContext : DbContext
    {
        public const string ConnectionString =
           "Server=.;Database=SandboxGame;Integrated Security=true;";

        public SandBoxGameDbContext()
        {

        }
        public SandBoxGameDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
    }
}
