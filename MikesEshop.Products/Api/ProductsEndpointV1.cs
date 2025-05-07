using Mapster;
using MikesEshop.Products.Api.Requests;
using MikesEshop.Products.Api.Responses;
using MikesEshop.Products.Application.Commands;
using MikesEshop.Products.Application.Queries;
using MikesEshop.Products.Core;
using MikesEshop.Products.Core.Events;
using Wolverine;
using Wolverine.Http;

namespace MikesEshop.Products.Api;

public class ProductsEndpointV1
{
    [WolverinePost("/v1/products")]
    public static async Task<CreateProductResponse> CreateProduct(CreateProductRequest request, IMessageBus bus)
    {
        var command = request.Adapt<CreateProductCommand>();
        var productCreatedEvent = await bus.InvokeAsync<ProductCreated>(command);
        
        return productCreatedEvent.Adapt<CreateProductResponse>();
    }
    
    [WolverinePatch("/v1/products/{id}/stock")]
    public static async Task<UpdateProductStockResponse> UpdateProductStock(Guid id, UpdateProductStockRequest request, IMessageBus bus)
    {
        var command = new UpdateProductStockCommand(id, request.NewQuantity);
        var productStockChangedEvent = await bus.InvokeAsync<ProductStockChanged>(command);

        return productStockChangedEvent.Adapt<UpdateProductStockResponse>();
    }
    
    [WolverineGet("/v1/products")]
    public static async Task<IReadOnlyList<Product>> GetAllStockedProducts(IMessageBus bus)
    {
        var query = new GetAllStockedProductsQuery();
        var getAllProductsQueryResponse = await bus.InvokeAsync<GetAllStockedProductsQueryResponse>(query);

        return getAllProductsQueryResponse.Products;
    }
    
    [WolverineGet("/v1/products/{id}")]
    public static async Task<Product?> GetProductById(Guid id, IMessageBus bus)
    {
        var query = new GetProductByIdQuery(id);
        var getProductByIdQueryResponse = await bus.InvokeAsync<GetProductByIdQueryResponse>(query);

        return getProductByIdQueryResponse.Product;
    }
}