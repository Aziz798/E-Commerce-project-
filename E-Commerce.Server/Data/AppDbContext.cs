using E_Commerce.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        // Disable lazy loading proxies
        ChangeTracker.LazyLoadingEnabled = false;
    }
    public DbSet<User> Users { get; set; }=null!;
    public DbSet<Product> Products { get; set; } =null!;
    public DbSet<ProductPhoto> Photos { get; set; } = null!;

}
