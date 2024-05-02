using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Core.Products;

namespace WebApiTemplate.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    // Add other DbSets for other entity models
}
