using MikesEshop.Products.Application.Commands;
using MikesEshop.Products.Core;
using MikesEshop.Shared.Application.Services;
using NSubstitute;

namespace MikesEshop.Products.UnitTests;

public class UpdateProductStockHandlerTests
{
    private readonly IRepository<Product> productRepositoryMock = Substitute.For<IRepository<Product>>();
    private readonly CancellationToken cancellationToken = CancellationToken.None;

    [Fact]
    public async Task Handle_ExistingProduct_ShouldReturnUpdatedStock()
    {
        const int newStock = 20;
        var product = new Product("Test Product", "image.png", stockedQuantity: 10);
        var command = new UpdateProductStockCommand(product.Id, newStock);
        
        var result = await UpdateProductStockCommandHandler.Handle(
            command,
            product,
            productRepositoryMock,
            cancellationToken);
        
        productRepositoryMock.Received(1).Update(product);
        await productRepositoryMock.Received(1).CommitAsync(cancellationToken);
        
        Assert.Equal(newStock, product.StockedQuantity);
        Assert.Equal(newStock, result.StockedQuantity);
    }
    
    [Fact]
    public async Task Handle_NegativeQuantity_ShouldThrow()
    {
        const int newStock = -20;
        var product = new Product("Test Product", "image.png", stockedQuantity: 10);
        var command = new UpdateProductStockCommand(product.Id, newStock);
        
        var handleAction = () => UpdateProductStockCommandHandler.Handle(
            command,
            product,
            productRepositoryMock,
            cancellationToken);
        
        await Assert.ThrowsAsync<ArgumentException>(handleAction);
    }
    
    [Fact]
    public async Task LoadAsync_ExistingProduct_ShouldReturnProduct()
    {
        var product = new Product("Test Product", "image.png");
        var command = new UpdateProductStockCommand(product.Id, 10);

        productRepositoryMock.GetByIdAsync(command.ProductId, cancellationToken)
            .Returns(product);

        var result = await UpdateProductStockCommandHandler.LoadAsync(command, productRepositoryMock, cancellationToken);
        
        await productRepositoryMock.Received(1).GetByIdAsync(command.ProductId, cancellationToken);
        Assert.Equal(product.Id, result.Id);
        Assert.Equal(product.Name, result.Name);
    }

    [Fact]
    public async Task LoadAsync_NonExistingProduct_ShouldThrow()
    {
        var command = new UpdateProductStockCommand(Guid.NewGuid(), 10);

        productRepositoryMock.GetByIdAsync(command.ProductId, cancellationToken)
            .Returns((Product?)null);

        var handleAction = () => 
            UpdateProductStockCommandHandler.LoadAsync(command, productRepositoryMock, cancellationToken);

        await Assert.ThrowsAsync<ArgumentNullException>(handleAction);
    }
}