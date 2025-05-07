namespace MikesEshop.Products.Core.Events;

public record ProductCreated(Guid Id, string Name, string ImageUrl);