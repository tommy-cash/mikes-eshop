using MikesEshop.Products.Api.Dtos.Requests;
using MikesEshop.Products.Api.Dtos.Responses;
using MikesEshop.Products.Application.Commands;
using MikesEshop.Products.Core;
using MikesEshop.Products.Core.Events;

namespace MikesEshop.Products.Api.Dtos.Mappers;

public static class ProductDtosMapperExtensions
{
    public static CreateProductResponseDto MapToCreateProductResponseDto(this ProductCreated productCreated)
    {
        return new CreateProductResponseDto(productCreated.Id, productCreated.Name, productCreated.ImageUrl);
    }
    
    public static CreateProductCommand MapToCreateProductCommand(this CreateProductRequestDto request)
    {
        return new CreateProductCommand(
            request.Name,
            request.ImageUrl,
            request.Description,
            request.StockedQuantity,
            request.Price,
            request.Dimensions);
    }
    
    public static UpdateProductStockResponseDto MapToUpdateProductStockCommand(this ProductStockChanged productStockChanged)
    {
        return new UpdateProductStockResponseDto(
            productStockChanged.Id,
            productStockChanged.Name,
            productStockChanged.ImageUrl);
    }

    public static ProductListDto MapToProductListDto(this Product product)
    {
        return new ProductListDto(
            product.Id,
            product.Name,
            product.ImageUrl,
            product.Price?.Amount,
            product.Price?.Currency,
            product.StockedQuantity);
    }
    
    public static ProductDetailDto MapToProductDetailDto(this Product product)
    {
        return new ProductDetailDto(
            product.Id,
            product.Name,
            product.Description,
            product.ImageUrl,
            product.Price?.Amount,
            product.Price?.Currency,
            product.StockedQuantity,
            product.Dimensions?.Width,
            product.Dimensions?.Height,
            product.Dimensions?.Depth);
    }
}