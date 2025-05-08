using Ardalis.GuardClauses;
using MikesEshop.Products.Application.Mappers;
using MikesEshop.Products.Core;
using MikesEshop.Products.Core.Events;
using MikesEshop.Shared.Application.Services;

namespace MikesEshop.Products.Application.Commands;

public record UpdateProductStockCommand(Guid ProductId, int NewQuantity);

public static class UpdateProductStockCommandHandler
{
    public static async Task<Product> LoadAsync(
        UpdateProductStockCommand command,
        IRepository<Product> repository,
        CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(command.ProductId, cancellationToken);
        Guard.Against.Null(product, "Product not found");

        return product;
    }

    public static async Task<ProductStockChanged> Handle(
        UpdateProductStockCommand command,
        Product product,
        IRepository<Product> repository,
        CancellationToken cancellationToken)
    {
        product.UpdateStock(command.NewQuantity);

        repository.Update(product);
        await repository.CommitAsync(cancellationToken);

        return product.MapToProductStockChanged();
    }
}