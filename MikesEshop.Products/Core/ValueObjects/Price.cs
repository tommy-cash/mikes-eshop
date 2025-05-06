using Ardalis.GuardClauses;

namespace MikesEshop.Products.Core.ValueObjects;

public record Price
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }

    private Price() { }

    public Price(decimal amount, string currency)
    {
        Guard.Against.Negative(amount, nameof(amount), "Product price cannot be negative");
        
        Amount = amount;
        Currency = currency;
    }
}