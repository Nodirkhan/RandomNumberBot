using Microsoft.EntityFrameworkCore;
using RandomNumberBot.Entity;
using System.Xml.XPath;

namespace RandomNumberBot
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<RegionCounter> RegionCounters { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OldUser> OldUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(DefaultValues.CONNECTION);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DefaultRegionCounter());
        }
    }
}
