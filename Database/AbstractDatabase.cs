using Microsoft.SqlServer.Management.Smo;

namespace Database
{
    public abstract class AbstractDatabase
    {
        protected Server Server;
        public Microsoft.SqlServer.Management.Smo.Database Database;

        public AbstractDatabase(string databaseName)
        {
            Server = new Server();
            Database = Server.Databases[databaseName];
        }


    }
}
