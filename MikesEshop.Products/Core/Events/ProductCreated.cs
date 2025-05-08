using MikesEshop.Products.Core.ValueObjects;

namespace MikesEshop.Products.Core.Events;

public record ProductCreated(Guid Id, string Name, string ImageUrl, string? Description, int? StockedQuantity, Price? Price, Dimensions? Dimensions);