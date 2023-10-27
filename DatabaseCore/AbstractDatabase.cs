using Common.Logging;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace DatabaseCore;

public abstract class AbstractDatabase
{
    protected Server Server;
    public Database Database;
    public string DatabaseName { get; set; }
    public string ConnectionString { get; set; }

    public AbstractDatabase(string databaseName)
    {
        // Get DOCKERIZED environment variable
        string? DockerizedEnviron = Environment.GetEnvironmentVariable("DOCKERIZED");
        if (DockerizedEnviron == null) // Variable not set
        {
            ConnectionString = $"Data Source=localhost;database={databaseName};IntegratedSecurity=True;TrustServerCertificate=True;";
        }
        else if (DockerizedEnviron == "Dockerized") // Is dockerized
        {
            ConnectionString = $"Server=sql_server;database={databaseName};User Id=SA;Password=BigPass@Word!;TrustServerCertificate=True;";
        }
        else // Fallback
        {
            ConnectionString = $"Data Source=localhost;database={databaseName};IntegratedSecurity=True;TrustServerCertificate=True;";
        }

        LogWriter.LogInfo($"DB Connection: {ConnectionString}");

        DatabaseName = databaseName;
        Server = new Server(new ServerConnection(new SqlConnection(ConnectionString)));
        Database = Server.Databases[databaseName];
    }

}
