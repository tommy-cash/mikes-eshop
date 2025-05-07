namespace MikesEshop.Products.Application.Commands;

public record UpdateProductStockCommand(Guid ProductId, int NewQuantity);