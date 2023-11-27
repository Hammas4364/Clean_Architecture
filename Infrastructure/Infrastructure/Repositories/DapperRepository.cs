namespace Infrastructure.Repositories;
using System.Data.Common;
using Application.Interfaces.Repositories;
using Microsoft.Data.SqlClient;
using Application.Common.DBConnection;

internal class DapperRepository : IDapperRepository
{
    private readonly IDBConnection _dbConnection;
    public DapperRepository(IDBConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public DbConnection GetConnection()
    {
        var con = new SqlConnection(_dbConnection.CS);
        return con;
    }
}
