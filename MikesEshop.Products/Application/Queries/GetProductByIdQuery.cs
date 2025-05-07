using MikesEshop.Products.Core;

namespace MikesEshop.Products.Application.Queries;

public record GetProductByIdQuery(Guid ProductId);
public record GetProductByIdQueryResponse(Product? Product);
