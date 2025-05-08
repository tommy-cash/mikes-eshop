using MikesEshop.Shared.Core;
using MikesEshop.Shared.Infrastracture;

namespace MikesEshop.Products.UnitTests.MockedObjects;

public class TestQueryObject<TAggregate> : QueryObject<TAggregate> where TAggregate : AggregateRoot
{
    public TestQueryObject(IQueryable<TAggregate> initialQueryable)
    {
        Query = initialQueryable;
    }
    
    public override Task<IEnumerable<TAggregate>> ExecuteAsync()
    {
        return Task.FromResult(Query.AsEnumerable());
    }
}