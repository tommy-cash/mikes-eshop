using MikesEshop.Products.Core;
using MikesEshop.Shared.Application.Services;

namespace MikesEshop.Products.Application.Queries;

public record GetAllStockedProductsPagedQuery(int Page, int PageSize);
public record GetAllStockedProductsPagedQueryResponse(List<Product> Products);

public static class GetAllStockedProductsPagedQueryHandler
{
    public static async Task<GetAllStockedProductsPagedQueryResponse> Handle(
        GetAllStockedProductsPagedQuery query,
        IQueryObject<Product> queryObject)
    {
        var page = query.Page == default ? 1 : query.Page;
        var pageSize = query.PageSize == default ? 10 : query.PageSize;
        
        var products = await queryObject
            .Filter(x => x.StockedQuantity > 0)
            .Page(page, pageSize)
            .ExecuteAsync();
        
        return new GetAllStockedProductsPagedQueryResponse(products.ToList());
    }
}