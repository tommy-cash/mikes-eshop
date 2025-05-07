using MikesEshop.Shared.Core;

namespace MikesEshop.Shared.Application.Services;

public interface IRepository<TAggregate> where TAggregate : AggregateRoot
{
    Task<TAggregate> AddAsync(TAggregate entity, CancellationToken cancellationToken = default);
    Task<TAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Update(TAggregate entity);
    Task<bool> RemoveAsync(object id, CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
}