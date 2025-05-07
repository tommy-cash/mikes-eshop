using MikesEshop.Products.Core.ValueObjects;

namespace MikesEshop.Products.Api.Requests;

public record CreateProductRequest(
    string Name,
    string ImageUrl,
    string? Description = null,
    int? StockedQuantity = null,
    Price? Price = null,
    Dimensions? Dimensions = null);