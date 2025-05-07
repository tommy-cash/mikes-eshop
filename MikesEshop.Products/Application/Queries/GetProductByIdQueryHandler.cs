using MikesEshop.Products.Infrastructure;

namespace MikesEshop.Products.Application.Queries;

public class GetProductByIdQueryHandler
{
    public static async Task<GetProductByIdQueryResponse> Handle(
        GetProductByIdQuery query,
        ProductsDbContext dbContext)
    {
        var product = await dbContext.Products.FindAsync(query.ProductId);

        return new GetProductByIdQueryResponse(product);
    }
}