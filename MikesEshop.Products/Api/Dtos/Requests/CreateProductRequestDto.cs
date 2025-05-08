using MikesEshop.Products.Core.ValueObjects;

namespace MikesEshop.Products.Api.Dtos.Requests;

public record CreateProductRequestDto(
    string Name,
    string ImageUrl,
    string? Description = null,
    int? StockedQuantity = null,
    Price? Price = null,
    Dimensions? Dimensions = null);