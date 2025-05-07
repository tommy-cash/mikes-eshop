using MikesEshop.Products.Core;

namespace MikesEshop.Products.Application.Queries;

public record GetAllStockedProductsQuery();
public record GetAllStockedProductsQueryResponse(List<Product> Products);
