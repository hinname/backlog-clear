using BacklogClear.Application.UseCases.Games.Register;
using BacklogClear.Communication.Requests;
using BacklogClear.Communication.Responses;
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
            var errorResponse = new ResponseErrorJson(ex.Message);
            return BadRequest(errorResponse);
        }
        catch (Exception ex)
        {
            var errorResponse = new ResponseErrorJson(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }
        
    }
}

