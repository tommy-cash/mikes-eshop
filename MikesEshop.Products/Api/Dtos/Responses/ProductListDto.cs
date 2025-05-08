namespace MikesEshop.Products.Api.Dtos.Responses;

public record ProductListDto(
    Guid Id,
    string Name,
    string ImageUrl,
    decimal? Price,
    string? Currency,
    int? StockQuantity);
