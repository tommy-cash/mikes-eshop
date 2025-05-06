using Ardalis.GuardClauses;

namespace MikesEshop.Products.Core.ValueObjects;

public record Dimensions
{
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public decimal Depth { get; init; }

    private Dimensions() { }

    public Dimensions(decimal width, decimal height, decimal depth)
    {
        Guard.Against.NegativeOrZero(width, nameof(width), "Product width cannot be negative");
        Guard.Against.NegativeOrZero(height, nameof(height), "Product height cannot be negative");
        Guard.Against.NegativeOrZero(depth, nameof(depth), "Product depth cannot be negative");
        
        Width = width;
        Height = height;
        Depth = depth;
    }
}