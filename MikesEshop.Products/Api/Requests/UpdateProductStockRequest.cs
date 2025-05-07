namespace MikesEshop.Products.Api.Requests;

public record UpdateProductStockRequest(Guid ProductId, int NewQuantity);