namespace Application.Specifications.Base;
using Dapper;
using SharedKernel.Constants;
using System.Data;

public interface IDSpecification<T>
{
    public string CommandText { get; }
    public object Parameters { get; }
    public CommandType CommandType { get; }
    public CommandDefinition AsCommandDefination(CancellationToken cancellationToken = default)
    {
        return new CommandDefinition(CommandText, Parameters, commandTimeout: Config.CommandTimeout, commandType: CommandType, cancellationToken: cancellationToken);
    }
}