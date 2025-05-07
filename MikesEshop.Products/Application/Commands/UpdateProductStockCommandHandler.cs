using Ardalis.GuardClauses;
using Mapster;
using MikesEshop.Products.Core;
using MikesEshop.Products.Core.Events;
using MikesEshop.Products.Infrastructure;

namespace MikesEshop.Products.Application.Commands;

public class UpdateProductStockCommandHandler
{
    public static async Task<Product> LoadAsync(
        UpdateProductStockCommand command,
        ProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync(command.ProductId, cancellationToken);
        Guard.Against.Null(product, "Product not found");

        return product;
    }

    public static async Task<ProductStockChanged> Handle(
        UpdateProductStockCommand command,
        Product product,
        ProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        product.UpdateStock(command.NewQuantity);

        dbContext.Update(product);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return product.Adapt<ProductStockChanged>();
    }
}