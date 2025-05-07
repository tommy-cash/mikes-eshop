using MikesEshop.Products.Core;

namespace MikesEshop.Products.Application.Queries;

public record GetAllProductsQuery();
public record GetAllProductsQueryResponse(List<Product> Products);
