using Bogus;
using Microsoft.EntityFrameworkCore;
using MikesEshop.Products.Core;
using MikesEshop.Products.Core.ValueObjects;

namespace MikesEshop.Products.Infrastructure.Seeds;

public static class ProductSeeder
{
    public static async Task EnsureSeededAsync(ProductsDbContext dbContext)
    {
        if (await dbContext.Products.AnyAsync())
            return;

        var productSeedFactory = new Faker<Product>("cz")
            .CustomInstantiator(f =>
                new Product(
                    name: f.Commerce.ProductName(),
                    imageUrl: f.Image.PicsumUrl(width: 400, height: 400),
                    description: f.Commerce.ProductDescription(),
                    stockedQuantity: f.Random.Int(0, 100),
                    price: new Price(f.Random.Decimal(10, 1000), "Kč"),
                    dimensions: new Dimensions(f.Random.Decimal(20, 200), f.Random.Decimal(20, 200),
                        f.Random.Decimal(20, 200)))
            );

        var products = productSeedFactory.Generate(30);

        dbContext.Products.AddRange(products);
        await dbContext.SaveChangesAsync();
    }
}