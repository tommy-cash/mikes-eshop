using Microsoft.EntityFrameworkCore;
using MikesEshop.Products.Infrastructure;

namespace MikesEshop.Products.Application.Queries;

public class GetAllProductsQueryHandler
{
    public static async Task<GetAllProductsQueryResponse> Handle(GetAllProductsQuery query, ProductsDbContext dbContext)
    {
        var products = await dbContext.Products.ToListAsync();

        return new GetAllProductsQueryResponse(products);
    }
}