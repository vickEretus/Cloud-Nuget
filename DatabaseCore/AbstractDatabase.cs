using Microsoft.SqlServer.Management.Smo;

namespace Database
{
    public abstract class AbstractDatabase
    {
        protected Server Server;
        public Microsoft.SqlServer.Management.Smo.Database Database;
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }

        public AbstractDatabase(string databaseName)
        {
            DatabaseName = databaseName;
            Server = new Server();
            Database = Server.Databases[databaseName];
            ConnectionString = $"Data Source=localhost;database={databaseName};Integrated Security=True;TrustServerCertificate=True;";
        }

    }
}
