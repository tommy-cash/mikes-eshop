using MikesEshop.Products.Core;
using MikesEshop.Shared.Application.Services;

namespace MikesEshop.Products.Application.Queries;

public record GetAllStockedProductsQuery();
public record GetAllStockedProductsQueryResponse(List<Product> Products);

public static class GetAllStockedProductsQueryHandler
{
    public static async Task<GetAllStockedProductsQueryResponse> Handle(
        GetAllStockedProductsQuery query,
        IQueryObject<Product> queryObject)
    {
        var products = await queryObject.Filter(x => x.StockedQuantity > 0).ExecuteAsync();

        return new GetAllStockedProductsQueryResponse(products.ToList());
    }
}