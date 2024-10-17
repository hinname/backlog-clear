using Microsoft.AspNetCore.Mvc;

namespace BacklogClear.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    
    private class User
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
    
    
    [HttpGet]
    [ProducesResponseType(typeof(User),StatusCodes.Status200OK)]
    public IActionResult GetUser()
    {
        var response = new User()
        {
            Email = "Teste@tes.com",
            Name = "Herivelton"
        };
        return Ok(response);
    }
}