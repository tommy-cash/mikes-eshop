using Mapster;
using MikesEshop.Products.Core;
using MikesEshop.Products.Core.Events;
using MikesEshop.Shared.Application.Services;

namespace MikesEshop.Products.Application.Commands;

public class CreateProductCommandHandler
{
    public static async Task<ProductCreated> Handle(
        CreateProductCommand command,
        IRepository<Product> repository,
        CancellationToken cancellationToken)
    {
        var product = command.Adapt<Product>();
        
        await repository.AddAsync(product, cancellationToken);
        await repository.CommitAsync(cancellationToken);

        return product.Adapt<ProductCreated>();
    }
}