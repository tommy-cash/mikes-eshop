using MikesEshop.Products.Application.Queries;
using MikesEshop.Products.Core;
using MikesEshop.Products.UnitTests.MockedObjects;

namespace MikesEshop.Products.UnitTests;

public class GetAllStockedProductsPagedHandlerTests
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
    
    [Theory]
    [InlineData(1, 2, 0, 1)]
    [InlineData(2, 2, 2, 3)]
    [InlineData(1, 3, 0, 1, 2)]
    [InlineData(2, 3, 3, 4, 5)]
    public async Task Handle_AllStockedProducts_ShouldReturnCorrectPage(int page, int pageSize, params int[] expectedIndexes)
    {
        var products = new List<Product>
        {
            new("Product 1", "image1.png", stockedQuantity: 10),
            new("Product 2", "image2.png", stockedQuantity: 2),
            new("Product 3", "image3.png", stockedQuantity: 5),
            new("Product 4", "image4.png", stockedQuantity: 3),
            new("Product 5", "image5.png", stockedQuantity: 7),
            new("Product 6", "image6.png", stockedQuantity: 1),
        };
        var productQueryObjectMock = new TestQueryObject<Product>(products.AsQueryable());
        var query = new GetAllStockedProductsPagedQuery(page, pageSize);

        var result = await GetAllStockedProductsPagedQueryHandler.Handle(query, productQueryObjectMock);

        Assert.Equal(expectedIndexes.Length, result.Products.Count);
        for (var i = 0; i < expectedIndexes.Length; i++)
        {
            Assert.Equal(products.ElementAt(expectedIndexes[i]).Id, result.Products.ElementAt(i).Id);
        }
    }
    
    [Fact]
    public async Task Handle_AllStockedProducts_ShouldReturnPartialSecondPage()
    {
        const int page = 2;
        const int pageSize = 2;
        var products = new List<Product>
        {
            new("Product 1", "image1.png", stockedQuantity: 10),
            new("Product 2", "image2.png", stockedQuantity: 2),
            new("Product 3", "image3.png", stockedQuantity: 5),
        };
        var productQueryObjectMock = new TestQueryObject<Product>(products.AsQueryable());
        var query = new GetAllStockedProductsPagedQuery(page, pageSize);

        var result = await GetAllStockedProductsPagedQueryHandler.Handle(query, productQueryObjectMock);

        Assert.Single(result.Products);
        Assert.Equal(products.ElementAt(2).Id, result.Products.FirstOrDefault()?.Id);
    }
}