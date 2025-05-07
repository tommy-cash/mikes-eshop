using Microsoft.EntityFrameworkCore;
using MikesEshop.Shared.Application.Services;
using MikesEshop.Shared.Core;

namespace MikesEshop.Products.Infrastructure.Services;

public class EfCoreRepository<TAggregate> : IRepository<TAggregate> where TAggregate : AggregateRoot
{
    protected readonly ProductsDbContext DbContext;
    protected readonly DbSet<TAggregate> DbSet;

    public EfCoreRepository(ProductsDbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<TAggregate>();
    }

    public async Task<TAggregate> AddAsync(TAggregate entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        return entity;
    }
    
    // TODO: add generic parameter for the type of id
    public async Task<TAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FindAsync(id, cancellationToken);
    }
    
    public void Update(TAggregate entity)
    {
        DbSet.Attach(entity);
        DbContext.Entry(entity).State = EntityState.Modified;
    }

    public async Task<bool> RemoveAsync(object id, CancellationToken cancellationToken = default)
    {
        var entity = await DbSet.FindAsync(id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        DbSet.Remove(entity);
        return true;
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default) 
        => await DbContext.SaveChangesAsync(cancellationToken);

}