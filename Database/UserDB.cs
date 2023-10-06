using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class UserDB : AbstractDatabase
    {
        public UserDB() : base("revmetrix-u")
        {

        }

        public async override Task<bool> Reset()
        {
            await Connection.OpenAsync();

            SqlCommand command = Connection.CreateCommand();

            // Disable foreign key constraints
            command.CommandText = "EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'";
            command.ExecuteNonQuery();

            // Delete data from all tables
            command.CommandText = "EXEC sp_MSforeachtable 'DELETE FROM ?'";
            command.ExecuteNonQuery();

            // Enable foreign key constraints
            command.CommandText = "EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL'";
            command.ExecuteNonQuery();

            var sqlCommand2 = new SqlCommand("CREATE TABLE Users(Username int, Email varchar(255));", Connection);

            Console.WriteLine(await sqlCommand2.ExecuteReaderAsync());
            
            Connection.Close();

            return true;
        }
    }
}
