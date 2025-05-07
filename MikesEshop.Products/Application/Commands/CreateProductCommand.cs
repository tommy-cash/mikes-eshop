using MikesEshop.Products.Core.ValueObjects;

namespace MikesEshop.Products.Application.Commands;

public record CreateProductCommand(
    string Name,
    string ImageUrl,
    string? Description = null,
    int? StockedQuantity = null,
    Price? Price = null,
    Dimensions? Dimensions = null);