using System;
using System.Data.SqlClient;

internal class Program
{
    private static void Main(string[] args)
    {
        string connectionString;
        {
            string databaseName = "revmetrix-u";
            connectionString = $"Server=localhost;database={databaseName};Integrated Security=True;";
        }

        using (var connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
