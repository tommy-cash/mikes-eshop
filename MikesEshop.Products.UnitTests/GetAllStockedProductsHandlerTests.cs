using MikesEshop.Products.Application.Queries;
using MikesEshop.Products.Core;
using MikesEshop.Products.UnitTests.MockedObjects;

namespace MikesEshop.Products.UnitTests;

public class GetAllStockedProductsHandlerTests
{
    [Fact]
    public async Task Handle_SomeStockedProducts_ShouldReturnOnlyStocked()
    {
        var products = new List<Product>
        {
            new("Product 1", "image1.png", stockedQuantity: 10),
            new("Product 2", "image2.png", stockedQuantity: 0),
            new("Product 3", "image3.png", stockedQuantity: 5),
            new("Product 4", "image4.png", stockedQuantity: 0),
        };
        var productQueryObjectMock = new TestQueryObject<Product>(products.AsQueryable());
        var query = new GetAllStockedProductsQuery();

        var result = await GetAllStockedProductsQueryHandler.Handle(query, productQueryObjectMock);

        Assert.Equal(2, result.Products.Count);
        Assert.All(result.Products, p => Assert.True(p.StockedQuantity > 0));
    }
}