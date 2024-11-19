using BacklogClear.Application.UseCases.Games.Register;
using BacklogClear.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BacklogClear.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    [HttpPost]
    public IActionResult RegisterGame([FromBody] RequestRegisterGameJson request)
    {
        try
        {
            var useCase = new RegisterGameUseCase();
            var response = useCase.Execute(request);
            return Created(string.Empty, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
        
    }
}

