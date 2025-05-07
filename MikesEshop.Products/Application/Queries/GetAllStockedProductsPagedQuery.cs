using MikesEshop.Products.Core;

namespace MikesEshop.Products.Application.Queries;

public record GetAllStockedProductsPagedQuery(int Page, int PageSize);
public record GetAllStockedProductsPagedQueryResponse(List<Product> Products);
