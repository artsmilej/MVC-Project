using Microsoft.EntityFrameworkCore;
using ProductDirectoryApp.Models;

namespace ProductDirectoryApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Firm> Firms { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}