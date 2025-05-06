using Microsoft.EntityFrameworkCore;
using MikesEshop.Products.Core;

namespace MikesEshop.Products.Infrastructure;

public class ProductsDbContext(DbContextOptions<ProductsDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().OwnsOne(p => p.Price);
        modelBuilder.Entity<Product>().OwnsOne(p => p.Dimensions);
        
        base.OnModelCreating(modelBuilder);
    }
}