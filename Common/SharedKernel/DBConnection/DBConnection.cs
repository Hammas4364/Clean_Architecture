using Microsoft.Extensions.Options;

namespace SharedKernel.DBConnection;

public class ConnectionStringsOption
{
    public const string SectionName = "ConnectionStrings";
    public string DefaultConnection { get; set; } = "";
    public string CS => DefaultConnection;

}

public class DBConnection : IDBConnection
{
    private string _connection = "";
    public DBConnection(IOptions<ConnectionStringsOption> options)
    {
        _connection = options.Value.CS;
        // _connection = "SERVER=" + "SYNERGY-SD" + ";" + "DATABASE=" + "AxisController" + ";" + "UID=" + "sa" + ";" + "PASSWORD=" + "DB2axxess" + ";MultipleActiveResultSets=true;";
    }

    public string CS
    {
        get { return _connection; }
        set { _connection = value; }
    }
}

