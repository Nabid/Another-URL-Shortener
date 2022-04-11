using Microsoft.EntityFrameworkCore;

namespace Another_URL_Shortener.Models
{
    public class ShortURLDbContext : DbContext
    {
        public ShortURLDbContext(DbContextOptions<ShortURLDbContext> options) : base(options)
        {
            
        }

        public DbSet<ShortUrl> ShortUrls { get; set; } = null!;

        // protected override void OnConfiguring(DbContextOptionsBuilder opions) => opions.UseSqlServer("Data Source=Another_URL_Shortener.db")

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<ShortUrl>().HasData(new ShortUrl[]{
                new ShortUrl{ URL="https://blog.jetbrains.com/dotnet/2020/11/25/getting-started-with-entity-framework-core-5/", ShortedURL="https://blog.jetbrains.com/12345", IsExpired=false }
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}