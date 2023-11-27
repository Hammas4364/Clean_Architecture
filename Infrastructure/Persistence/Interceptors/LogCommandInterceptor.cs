using Microsoft.EntityFrameworkCore.Diagnostics;
using Serilog;
using System.Data.Common;
namespace Persistence.Interceptors;

internal class LogCommandInterceptor : IDbCommandInterceptor
{
    public DbCommand CommandCreated(CommandEndEventData eventData, DbCommand result)
    {
        if (string.IsNullOrWhiteSpace(result.CommandText))
            return result!;
        if (Log.IsEnabled(Serilog.Events.LogEventLevel.Verbose))
            Log.Verbose("Created DbCommand: {@DbCommand}", result);
        else
            Log.Information("Created DbCommand: {CommandText} Duration:{Duration}", result.CommandText, eventData.Duration);

        return result;
    }

    public InterceptionResult<DbCommand> CommandCreating(CommandCorrelatedEventData eventData, InterceptionResult<DbCommand> result)
    {
        if (result.HasResult)
        {
            Log.Verbose("Creating DbCommand: {CommandText}", result.Result.CommandText);

            result.Result.Disposed += (o, e) => Log.Verbose("Disposed DbCommand: {CommandText}", result.Result.CommandText);
        }
        return result;
    }

    public void CommandFailed(DbCommand command, CommandErrorEventData eventData)
    {
        LogException(eventData);
    }

    private static void LogException(CommandErrorEventData eventData)
    {
        Log.Error("Message: {Message}\nStackTrace: {StackTrace}", eventData.Exception.Message, eventData.Exception.StackTrace);
    }

    public Task CommandFailedAsync(DbCommand command, CommandErrorEventData eventData, CancellationToken cancellationToken = default)
    {
        LogException(eventData);
        return Task.CompletedTask;
    }

    public InterceptionResult DataReaderDisposing(DbCommand command, DataReaderDisposingEventData eventData, InterceptionResult result)
    {
        Log.Verbose("Disposing DataReader DbCommand");
        return result;
    }

    public int NonQueryExecuted(DbCommand command, CommandExecutedEventData eventData, int result)
    {
        Log.Information("Executed NonQuery {{Duration:{Duration}ms}}: {CommandText}", eventData.Duration.TotalMilliseconds, command.CommandText);
        return result;
    }

    public async ValueTask<int> NonQueryExecutedAsync(DbCommand command, CommandExecutedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        Log.Information("Executed NonQueryAsync {{Duration:{Duration}ms}}\n{CommandText}", eventData.Duration.TotalMilliseconds, command.CommandText);
        return await Task.FromResult(result);
    }

    public InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
    {
        if (result.HasResult)
        {
            Log.Verbose("Executing NonQuery DbCommand Return Value: {result}", result);
            Log.Verbose("Executing NonQuery DbCommand: {@CommandEventData}", eventData);
        }

        return result;
    }

    public async ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (result.HasResult)
        {
            Log.Verbose("Executing NonQueryAsync DbCommand Return Value: {result}", result);
            Log.Verbose("Executing NonQueryAsync DbCommand: {@CommandEventData}", eventData);
        }

        return await Task.FromResult(result);
    }

    public DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
    {
        Log.Information("Executed Readers {{Duration:{Duration}ms}}\n{CommandText}", eventData.Duration.TotalMilliseconds, command.CommandText);
        return result;
    }

    public async ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result, CancellationToken cancellationToken = default)
    {
        Log.Information("Executed ReaderAsync {{Duration:{Duration}ms}}\n{CommandText}", eventData.Duration.TotalMilliseconds, command.CommandText);
        return await Task.FromResult(result);
    }

    public InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
    {
        if (result.HasResult)
        {
            Log.Verbose("Executing Readers DbCommand Return Value: {result}", result);
            Log.Verbose("Executing Readers DbCommand: {@CommandEventData}", eventData);
        }

        return result;
    }

    public async ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
    {
        if (result.HasResult)
        {
            Log.Verbose("Executing ReaderAsync DbCommand Return Value: {result}", result);
            Log.Verbose("Executing ReaderAsync DbCommand: {@CommandEventData}", eventData);
        }

        return await Task.FromResult(result);
    }

    public object? ScalarExecuted(DbCommand command, CommandExecutedEventData eventData, object? result)
    {
        Log.Information("Executed Scalar {{Duration:{Duration}ms}}\n{CommandText}", eventData.Duration.TotalMilliseconds, command.CommandText);
        return result;
    }

    public async ValueTask<object?> ScalarExecutedAsync(DbCommand command, CommandExecutedEventData eventData, object? result, CancellationToken cancellationToken = default)
    {
        Log.Information("Executed ScalarAsync {{Duration:{Duration}ms}}\n{CommandText}", eventData.Duration.TotalMilliseconds, command.CommandText);
        return await Task.FromResult(result);
    }

    public InterceptionResult<object> ScalarExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<object> result)
    {
        if (result.HasResult)
        {
            Log.Verbose("Executing Scalar DbCommand Return Value: {result}", result);
            Log.Verbose("Executing Scalar DbCommand: {CommandText}", command.CommandText);
        }

        return result;
    }

    public async ValueTask<InterceptionResult<object>> ScalarExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<object> result, CancellationToken cancellationToken = default)
    {
        if (result.HasResult)
        {
            Log.Verbose("Executing ScalarAsync DbCommand Return Value: {result}", result);
            Log.Verbose("Executing ScalarAsync DbCommand: {CommandText}", command.CommandText);
        }

        return await Task.FromResult(result);
    }
}
