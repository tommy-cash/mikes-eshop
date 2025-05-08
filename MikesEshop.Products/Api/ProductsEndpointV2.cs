using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MikesEshop.Products.Api.Dtos.Mappers;
using MikesEshop.Products.Api.Dtos.Requests;
using MikesEshop.Products.Api.Dtos.Responses;
using MikesEshop.Products.Application.Commands;
using MikesEshop.Products.Application.Queries;
using MikesEshop.Products.Core.Events;
using Wolverine;
using Wolverine.Http;

namespace MikesEshop.Products.Api;

public class ProductsEndpointV2
{
    [WolverinePost("/v2/products")]
    [EndpointSummary("Create new product")]
    public static async Task<CreateProductResponseDto> CreateProduct(CreateProductRequestDto requestDto, IMessageBus bus)
    {
        var command = requestDto.MapToCreateProductCommand();
        var productCreatedEvent = await bus.InvokeAsync<ProductCreated>(command);
        
        return productCreatedEvent.MapToCreateProductResponseDto();
    }
    
    [WolverinePatch("/v2/products/{id}/stock")]
    [EndpointSummary("Update product stock")]
    public static async Task<UpdateProductStockResponseDto> UpdateProductStock(
        Guid id,
        UpdateProductStockRequestDto requestDto,
        IMessageBus bus)
    {
        var command = new UpdateProductStockCommand(id, requestDto.NewQuantity);
        var productStockChangedEvent = await bus.InvokeAsync<ProductStockChanged>(command);

        return productStockChangedEvent.MapToUpdateProductStockCommand();
    }
    
    [WolverineGet("/v2/products")]
    [EndpointSummary("Get all paged available (stocked) products")]
    public static async Task<IReadOnlyList<ProductListDto>> GetAllStockedProducts(
        IMessageBus bus,
        [FromQuery] int page,
        [FromQuery] int pageSize)
    {
        var query = new GetAllStockedProductsPagedQuery(page, pageSize);
        var getAllProductsPagedQueryResponse = await bus.InvokeAsync<GetAllStockedProductsPagedQueryResponse>(query);

        return getAllProductsPagedQueryResponse.Products.Select(x => x.MapToProductListDto()).ToList();
    }
    
    [WolverineGet("/v2/products/{id}")]
    [EndpointSummary("Get single product by id")]
    public static async Task<ProductDetailDto?> GetProductById(Guid id, IMessageBus bus)
    {
        var query = new GetProductByIdQuery(id);
        var getProductByIdQueryResponse = await bus.InvokeAsync<GetProductByIdQueryResponse>(query);

        return getProductByIdQueryResponse.Product?.MapToProductDetailDto();
    }
}