using Common.Logging;
using Common.POCOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : AbstractFeaturedController
{
    [HttpGet("Test", Name = "Test")]
    public IActionResult Test()
    {
        LogWriter.LogInfo("Test called");
        return Ok();
    }

    [Authorize]
    [HttpGet("TestAuthorize", Name = "TestAuthorize")]
    public IActionResult TestAuthorize()
    {
        LogWriter.LogInfo("TestAuthorize called");
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("TestAuthenticateAdmin", Name = "TestAuthenticateAdmin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult TestAuthenticateAdmin()
    {
        LogWriter.LogInfo("TestAuthenticateAdmin called");
        return Ok();
    }

    [HttpGet("TestTime", Name = "TestTime")]
    public IActionResult TestTime()
    {
        LogWriter.LogInfo("TestTime called");
        return Ok(DateTimePoco.UTCNow);
    }
}
