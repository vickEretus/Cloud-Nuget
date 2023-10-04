using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
    class Program
    {
        static void Main(string[] args)
        {
             string connectionString = "Server=DESKTOP-AFT5UDN;Database=Testing;User Id=Jordan;Password=JORDAN;";
        // Attempting to make a connection to the mini    
        // string connectionString = "Server=DESKTOP-IBU02IL;Database=Testing;User Id=Ycp123;Password=Ycp123;";

        using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    int userId = 12; // Replace with the specific user ID you want to retrieve

                    string query = "SELECT * FROM [User] WHERE id = @UserId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserId", userId);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Console.WriteLine($"User ID: {reader["id"]}");
                        Console.WriteLine($"Name: {reader["name"]}");
                        Console.WriteLine($"Email: {reader["email"]}");
                        Console.WriteLine($"Phone: {reader["phone"]}");
                    }
                    else
                    {
                        Console.WriteLine("User not found.");
                    }

                    reader.Close();
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
