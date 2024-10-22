using Microsoft.AspNetCore.Mvc;

namespace BacklogClear.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
    
    
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(User),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string),StatusCodes.Status400BadRequest)]
    public IActionResult GetUser([FromHeader] string token, int id)
    {
        if (token != "Bearer 123456")
            return BadRequest("Token is invalid");
        
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
    
    [HttpPost]
    [ProducesResponseType(typeof(User),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult PostUser([FromBody] User user)
    {
        //CADASTRA USUÁRIO
        return Ok(user);
    }
}