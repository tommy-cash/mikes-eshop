using Ardalis.GuardClauses;
using MikesEshop.Products.Core.ValueObjects;
using MikesEshop.Shared.Core;

namespace MikesEshop.Products.Core;

public class Product : AggregateRoot
{
    public string Name { get; private set; }
    public string ImageUrl { get; private set; }
    public string? Description { get; private set; }
    public int? StockedQuantity { get; private set; }

    public Price? Price { get; private set; }
    public Dimensions? Dimensions { get; private set; }
    
    private Product() { }
     
    public Product(
        string name,
        string imageUrl,
        string? description = null,
        int? stockedQuantity = null,
        Price? price = null,
        Dimensions? dimensions = null)
    {
        Name = name;
        ImageUrl = imageUrl;
        Description = description;
        Price = price;
        StockedQuantity = stockedQuantity;
        Dimensions = dimensions;
    }

    public void UpdateStock(int quantity)
    {
        Guard.Against.Negative(quantity, nameof(quantity), "Quantity of stock cannot be negative");
        
        StockedQuantity = quantity;
    }
    
    public void UpdatePrice(decimal newAmount, string newCurrency)
    {
        Price = new Price(newAmount, newCurrency);
    }
    
    public void UpdateDimensions(decimal newWidth, decimal newHeight, decimal newDepth)
    {
        Dimensions = new Dimensions(newWidth, newHeight, newDepth);
    }
}