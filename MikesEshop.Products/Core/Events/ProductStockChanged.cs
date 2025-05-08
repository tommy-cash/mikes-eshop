namespace MikesEshop.Products.Core.Events;

public record ProductStockChanged(Guid Id, string Name, string ImageUrl, int StockedQuantity);