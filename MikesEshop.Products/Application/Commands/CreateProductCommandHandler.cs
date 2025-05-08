using MikesEshop.Products.Application.Mappers;
using MikesEshop.Products.Core;
using MikesEshop.Products.Core.Events;
using MikesEshop.Products.Core.ValueObjects;
using MikesEshop.Shared.Application.Services;

namespace MikesEshop.Products.Application.Commands;

public record CreateProductCommand(
    string Name,
    string ImageUrl,
    string? Description = null,
    int? StockedQuantity = null,
    Price? Price = null,
    Dimensions? Dimensions = null);

public static class CreateProductCommandHandler
{
    public static async Task<ProductCreated> Handle(
        CreateProductCommand command,
        IRepository<Product> repository,
        CancellationToken cancellationToken)
    {
        var product = command.MapToProduct();
        
        await repository.AddAsync(product, cancellationToken);
        await repository.CommitAsync(cancellationToken);

        return product.MapToProductCreated();
    }
}