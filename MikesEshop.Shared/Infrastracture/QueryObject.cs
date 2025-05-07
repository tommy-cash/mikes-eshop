using System.Linq.Expressions;
using MikesEshop.Shared.Application.Services;
using MikesEshop.Shared.Core;

namespace MikesEshop.Shared.Infrastracture;

public abstract class QueryObject<TAggregate> : IQueryObject<TAggregate> where TAggregate : AggregateRoot
{
    protected IQueryable<TAggregate> Query;
    protected readonly List<(Expression<Func<TAggregate, object>> selector, bool ascending)> SortingCriteria = [];

    public IQueryObject<TAggregate> Filter(Expression<Func<TAggregate, bool>> predicate)
    {
        Query = Query.Where(predicate);
        return this;
    }

    public IQueryObject<TAggregate> Page(int page, int pageSize)
    {
        Query = Query.Skip((page - 1) * pageSize).Take(pageSize);
        return this;
    }

    public IQueryObject<TAggregate> OrderBy(Expression<Func<TAggregate, object>> selector, bool ascending = true)
    {
        SortingCriteria.Add((selector, ascending));
        Query = ApplySorting();
        return this;
    }
    
    protected IQueryable<TAggregate> ApplySorting()
    {
        if (SortingCriteria.Count == 0)
            return Query;

        IOrderedQueryable<TAggregate>? orderedQuery = null;

        foreach (var criteria in SortingCriteria)
        {
            if (orderedQuery is null)
            {
                orderedQuery = criteria.ascending
                    ? Query.OrderBy(criteria.selector)
                    : Query.OrderByDescending(criteria.selector);
            }
            else
            {
                orderedQuery = criteria.ascending
                    ? orderedQuery.ThenBy(criteria.selector)
                    : orderedQuery.ThenByDescending(criteria.selector);
            }
        }

        return orderedQuery!;
    }

    public abstract Task<IEnumerable<TAggregate>> ExecuteAsync();
}