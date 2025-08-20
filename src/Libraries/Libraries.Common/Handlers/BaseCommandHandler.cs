using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Libraries.Common.Abstractions.Commands;
using Microsoft.Extensions.Logging;

namespace Libraries.Common.Handlers;

public abstract class BaseCommandHandler<TCommand, TResponse>(ILogger<BaseCommandHandler<TCommand, TResponse>> logger)
: ICommandHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : notnull
{
    private readonly ILogger<BaseCommandHandler<TCommand, TResponse>> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<TResponse> Handle(TCommand command, CancellationToken token = default)
    {
        _logger.LogInformation("[START] Handle Command={Request} - Response={Response} - RequestData={RequestData}",
            typeof(TCommand).Name, typeof(TResponse).Name, command);

        var timer = new Stopwatch();
        timer.Start();

        var response = await Execute(command, token);

        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 5)
            _logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken}",
                typeof(TCommand).Name, timeTaken.Seconds);

        _logger.LogInformation("[END] Handled {Request} with {Response}",
            typeof(TCommand).Name, typeof(TResponse).Name);

        return response;
    }

    public abstract Task<TResponse> Execute(TCommand command, CancellationToken token = default);
}
