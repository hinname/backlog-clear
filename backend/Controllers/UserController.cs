using Microsoft.AspNetCore.Mvc;

namespace BacklogClear.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    
    private class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
    
    
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(User),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string),StatusCodes.Status404NotFound)]
    public IActionResult GetUser(int id)
    {
        var response = new User()
        {
            Id = 1,
            Email = "Teste@tes.com",
            Name = "Herivelton"
        };
        if (id == response.Id)
            return Ok(response);
        
        return NotFound("User not found");
    }
}