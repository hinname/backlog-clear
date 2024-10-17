using Microsoft.AspNetCore.Mvc;

namespace BacklogClear.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet]
    public IActionResult GetUser()
    {
        return Ok("Hello World");
    }
}