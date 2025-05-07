using Mapster;
using MikesEshop.Products.Core;
using MikesEshop.Products.Core.Events;
using MikesEshop.Products.Infrastructure;

namespace MikesEshop.Products.Application.Commands;

public class CreateProductCommandHandler
{
    public static async Task<ProductCreated> Handle(
        CreateProductCommand command,
        ProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var product = command.Adapt<Product>();
        
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return product.Adapt<ProductCreated>();
    }
}