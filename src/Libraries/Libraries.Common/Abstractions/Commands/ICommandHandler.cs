using System.Threading;
using System.Threading.Tasks;

namespace Libraries.Common.Abstractions.Commands;

public interface ICommandHandler<in TCommand, TResponse> 
    where TCommand : ICommand<TResponse>
{
    Task<TResponse> Handle(TCommand command, CancellationToken token = default);
}
