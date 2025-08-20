using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Libraries.Common.Abstractions.Queries;
using Microsoft.Extensions.Logging;

namespace Libraries.Common.Handlers;

public abstract class BaseQueryHandler<TQuery, TResponse>(ILogger logger)
: IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : notnull
{
    private readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<TResponse> Handle(TQuery query, CancellationToken token = default)
    {
        _logger.LogInformation("[START] Handle Query={Request} - Response={Response} - RequestData={RequestData}",
            typeof(TQuery).Name, typeof(TResponse).Name, query);

        var timer = new Stopwatch();
        timer.Start();

        var response = await Execute(query, token);
        
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 5) 
            _logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken}",
                typeof(TQuery).Name, timeTaken.Seconds);

        _logger.LogInformation("[END] Handled {Request} with {Response}",
            typeof(TQuery).Name, typeof(TResponse).Name);

        return response;
    }

    public abstract Task<TResponse> Execute(TQuery query, CancellationToken token = default);
}
