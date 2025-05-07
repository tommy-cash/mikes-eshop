namespace MikesEshop.Shared.Core;

public abstract class EntityBase : IEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    
    protected EntityBase() { }
}