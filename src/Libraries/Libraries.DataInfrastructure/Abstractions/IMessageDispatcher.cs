using System.Data;
using System.Threading;
using System.Threading.Tasks;

using DotNetCore.CAP;

using Libraries.Common.Messages;

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Libraries.DataInfrastructure.Abstractions;

public interface IMessageDispatcher
{
    Task DispachAsync(IMessage message, CancellationToken token = default);
    Task<IDbContextTransaction> BeginTransactionAsync(DatabaseFacade databaseFacade, CancellationToken token = default);
    ValueTask<ICapTransaction> BeginTransactionAsync(IDbConnection connection, CancellationToken token = default);
}