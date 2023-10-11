using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Database
{
    public abstract class AbstractDatabase
    {
        private readonly string ConnectionString;
        public SqlConnection Connection;

        public AbstractDatabase(string databaseName)
        {
            ConnectionString = $"Server=localhost;database={databaseName};Integrated Security=True;";
            Connection = new SqlConnection(ConnectionString);
        }
    }
}
