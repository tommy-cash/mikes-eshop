using System.Linq.Expressions;
using MikesEshop.Shared.Core;

namespace MikesEshop.Shared.Application.Services;

public interface IQueryObject<TAggregate> where TAggregate : AggregateRoot
{
    IQueryObject<TAggregate> Filter(Expression<Func<TAggregate, bool>> predicate);
    IQueryObject<TAggregate> OrderBy(Expression<Func<TAggregate, object>> selector, bool ascending = true);
    IQueryObject<TAggregate> Page(int page, int pageSize);

    Task<IEnumerable<TAggregate>> ExecuteAsync();
}