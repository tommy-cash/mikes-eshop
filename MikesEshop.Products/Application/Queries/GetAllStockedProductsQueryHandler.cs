using MikesEshop.Products.Core;
using MikesEshop.Shared.Application.Services;

namespace MikesEshop.Products.Application.Queries;

public class GetAllStockedProductsQueryHandler
{
    public static async Task<GetAllStockedProductsQueryResponse> Handle(
        GetAllStockedProductsQuery query,
        IQueryObject<Product> queryObject)
    {
        var products = await queryObject.Filter(x => x.StockedQuantity > 0).ExecuteAsync();

        return new GetAllStockedProductsQueryResponse(products.ToList());
    }
}