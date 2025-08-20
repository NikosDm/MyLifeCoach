using System.Threading;
using System.Threading.Tasks;

namespace Libraries.Common.Abstractions.Queries;

public interface IQueryHandler<in TQuery, TResponse> 
    where TQuery : IQuery<TResponse>
    where TResponse : notnull
{
    Task<TResponse> Handle(TQuery query, CancellationToken token = default);
}
