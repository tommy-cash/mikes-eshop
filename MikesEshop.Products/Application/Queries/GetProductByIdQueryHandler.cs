using MikesEshop.Products.Core;
using MikesEshop.Shared.Application.Services;

namespace MikesEshop.Products.Application.Queries;

public record GetProductByIdQuery(Guid ProductId);
public record GetProductByIdQueryResponse(Product? Product);

public static class GetProductByIdQueryHandler
{
    public static async Task<GetProductByIdQueryResponse> Handle(
        GetProductByIdQuery query,
        IRepository<Product> repository)
    {
        var product = await repository.GetByIdAsync(query.ProductId);

        return new GetProductByIdQueryResponse(product);
    }
}