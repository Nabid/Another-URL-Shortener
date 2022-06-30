using Another_URL_Shortener.Models;
using Another_URL_Shortener.Responses;
using Microsoft.EntityFrameworkCore;

namespace Another_URL_Shortener.Configuration
{
    public class CacheDbContext : DbContext
    {
        public DbSet<CachedShortUrl> CachedShortUrls {get; set;}

        public CacheDbContext(DbContextOptions<CacheDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}