using MikesEshop.Products.Application.Commands;
using MikesEshop.Products.Core;
using MikesEshop.Products.Core.Events;

namespace MikesEshop.Products.Application.Mappers;

public static class ProductMapperExtensions
{
    public static Product MapToProduct(this CreateProductCommand command) 
    {
        return new Product(
            command.Name,
            command.ImageUrl,
            command.Description,
            command.StockedQuantity,
            command.Price,
            command.Dimensions);
    }
    
    public static ProductCreated MapToProductCreated(this Product product) 
    {
        return new ProductCreated(
            product.Id,
            product.Name,
            product.ImageUrl,
            product.Description,
            product.StockedQuantity,
            product.Price,
            product.Dimensions);
    }
    
    public static ProductStockChanged MapToProductStockChanged(this Product product)
    {
        return new ProductStockChanged(
            product.Id,
            product.Name,
            product.ImageUrl,
            product.StockedQuantity!.Value);
    }
}