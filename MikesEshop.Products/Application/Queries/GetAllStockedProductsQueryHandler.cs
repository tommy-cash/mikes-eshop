using Microsoft.EntityFrameworkCore;
using MikesEshop.Products.Infrastructure;

namespace MikesEshop.Products.Application.Queries;

public class GetAllStockedProductsQueryHandler
{
    public static async Task<GetAllStockedProductsQueryResponse> Handle(
        GetAllStockedProductsQuery query,
        ProductsDbContext dbContext)
    {
        var products = await dbContext.Products
            .Where(x => x.StockedQuantity > 0)
            .ToListAsync();

        return new GetAllStockedProductsQueryResponse(products);
    }
}