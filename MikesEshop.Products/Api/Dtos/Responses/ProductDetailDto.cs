namespace MikesEshop.Products.Api.Dtos.Responses;

public record ProductDetailDto(
    Guid Id,
    string Name,
    string? Description,
    string ImageUrl,
    decimal? Price,
    string? Currency,
    int? StockQuantity,
    decimal? Width,
    decimal? Height,
    decimal? Depth);