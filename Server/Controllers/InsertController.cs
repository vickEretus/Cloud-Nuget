using Microsoft.AspNetCore.Mvc;
using Database;
using System.Data.SqlClient;

[ApiController]
[Route("api/[controller]")]
public class InsertController : ControllerBase
{
    private readonly UserDB _userDB;

    public InsertController(UserDB userDB)
    {
        _userDB = userDB;
    }

    [HttpPost("CreateUser", Name = "CreateUser")]
    public async Task<IActionResult> CreateUser([FromBody] string name, [FromBody] string email, [FromBody] int phone_number)
    {
        if (name != null) 
        {
            try
            {
                await _userDB.Connection.OpenAsync();

                string insertQuery = "INSERT INTO [User] (name, email, phone) VALUES (@Name, @Email, @Phone)";
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, _userDB.Connection))
                {
                    insertCommand.Parameters.AddWithValue("@Name", name);
                    insertCommand.Parameters.AddWithValue("@Email", email);
                    insertCommand.Parameters.AddWithValue("@Phone", phone_number);
                    int rowsAffected = await insertCommand.ExecuteNonQueryAsync();
                }

                _userDB.Connection.Close();

                return Ok("User created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        return BadRequest("Invalid input");
    }

    // Other actions for updating and deleting items
}
