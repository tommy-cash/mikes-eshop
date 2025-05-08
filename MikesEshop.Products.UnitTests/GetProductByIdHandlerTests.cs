using MikesEshop.Products.Application.Queries;
using MikesEshop.Products.Core;
using MikesEshop.Shared.Application.Services;
using NSubstitute;

namespace MikesEshop.Products.UnitTests;

public class GetProductByIdHandlerTests
{
    private readonly IRepository<Product> productRepositoryMock = Substitute.For<IRepository<Product>>();
    private readonly CancellationToken cancellationToken = CancellationToken.None;
    
    [Fact]
    public async Task Handle_ExistingProduct_ShouldReturnProduct()
    {
        var products = new List<Product>
        {
            new("Product 1", "image1.png", stockedQuantity: 10),
            new("Product 2", "image2.png", stockedQuantity: 0),
            new("Product 3", "image3.png", stockedQuantity: 5),
            new("Product 4", "image4.png", stockedQuantity: 0),
        };
        productRepositoryMock.GetByIdAsync(products.First().Id, cancellationToken).Returns(x => products.First());
        var query = new GetProductByIdQuery(products.First().Id);

        var result = await GetProductByIdQueryHandler.Handle(query, productRepositoryMock);

        Assert.NotNull(result.Product);
        Assert.Equal(products.First().Id, result.Product?.Id);
    }
}