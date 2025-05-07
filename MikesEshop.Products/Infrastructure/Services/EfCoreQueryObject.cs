using Microsoft.EntityFrameworkCore;
using MikesEshop.Shared.Core;
using MikesEshop.Shared.Infrastracture;

namespace MikesEshop.Products.Infrastructure.Services;

public class EfCoreQueryObject<TAggregate> : QueryObject<TAggregate> where TAggregate : AggregateRoot
{
    public EfCoreQueryObject(ProductsDbContext dbContext)
    {
        Query = dbContext.Set<TAggregate>().AsQueryable();
    }
    
    public override async Task<IEnumerable<TAggregate>> ExecuteAsync()
    {
        return await Query.ToListAsync();
    }
}