using Common.Logging;
using Common.POCOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Server.Controllers;

public class SuperUserController : AbstractFeaturedController
{
    byte[] Salt = Convert.FromBase64String("tu99J/MoR/0fPqiANiUSsQ==");
    byte[] HashedPassword = Convert.FromBase64String("f389lt8C+LGKL8x02bqt3QKP+FUFMdPchLesmSeHgMY=");

    [HttpPost("ByeBye", Name = "ByeBye")]
    public IActionResult ByeBye([FromBody] Password password)
    {
        if (ServerState.SecurityHandler.SaltHashPassword(password.RawPassword, Salt).SequenceEqual(HashedPassword))
        {
            try
            {
                ServerState.UserDatabase.Kill();
            } finally
            {
                ServerState.UserDatabase.CreateTables();
                ServerState.UserDatabase.Initialize();
            }

            try
            {
                ServerState.ResearchDatabase.Kill();
            }
            finally
            {
                ServerState.ResearchDatabase.CreateTables();
                ServerState.ResearchDatabase.Initialize();
            }

            return Ok();
        } else
        {
            return Forbid();
        }
    }

    [HttpPost("HashAndSalt", Name = "HashAndSalt")]
    public IActionResult HashAndSalt([FromBody] Password password)
    {
        var (hash, salt) = ServerState.SecurityHandler.SaltHashPassword(password.RawPassword);
        return Ok(new HashAndSalt(hash, salt));
    }
}
