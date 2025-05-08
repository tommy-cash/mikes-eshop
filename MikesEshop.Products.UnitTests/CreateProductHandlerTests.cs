using MikesEshop.Products.Application.Commands;
using MikesEshop.Products.Core;
using MikesEshop.Products.Core.ValueObjects;
using MikesEshop.Shared.Application.Services;
using NSubstitute;

namespace MikesEshop.Products.UnitTests;

public class CreateProductHandlerTests
{
    private readonly IRepository<Product> productRepositoryMock = Substitute.For<IRepository<Product>>();
    private readonly CancellationToken cancellationToken = CancellationToken.None;

    [Fact]
    public async Task Handle_NewValidProduct_ShouldBeAddedToRepository()
    {
        var command = new CreateProductCommand("Test Product",
            "image.png",
            "Test Description",
            10,
            new Price(999.99m, "Kč"),
            new Dimensions(20, 10, 5));

        await CreateProductCommandHandler.Handle(
            command,
            productRepositoryMock,
            cancellationToken);

        await productRepositoryMock.Received(1).AddAsync(Arg.Is<Product>(p => 
            p.Name == command.Name &&
            p.ImageUrl == command.ImageUrl &&
            p.Description == command.Description && 
            p.Price == command.Price &&
            p.Dimensions == command.Dimensions),
            cancellationToken);
        
        await productRepositoryMock.Received(1).CommitAsync(cancellationToken);
    }
    
    [Fact]
    public async Task Handle_NewValidProduct_ShouldReturnProductCreatedEvent()
    {
        var command = new CreateProductCommand("Test Product",
            "image.png",
            "Test Description",
            10,
            new Price(999.99m, "Kč"),
            new Dimensions(20, 10, 5));

        var result = await CreateProductCommandHandler.Handle(
            command,
            productRepositoryMock,
            cancellationToken);

        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(command.Name, result.Name);
        Assert.Equal(command.ImageUrl, result.ImageUrl);
    }
    
    [Fact]
    public async Task Handle_NewInvalidProduct_ShouldThrow()
    {
        var command = new CreateProductCommand("Test Product",
            "image.png",
            "Test Description",
            -10,
            new Price(999.99m, "Kč"),
            new Dimensions(20, 10, 5));

        var handleAction = () => CreateProductCommandHandler.Handle(
            command,
            productRepositoryMock,
            cancellationToken);
        
        await Assert.ThrowsAsync<ArgumentException>(handleAction);
    }
}