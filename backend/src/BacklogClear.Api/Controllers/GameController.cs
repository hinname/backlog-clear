using BacklogClear.Application.UseCases.Games.Register;
using BacklogClear.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BacklogClear.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController: ControllerBase
{
    [HttpPost]
    public IActionResult RegisterGame(
        [FromServices] IRegisterGameUseCase useCase,
        [FromBody] RequestRegisterGameJson request
    )
    {
        var response = useCase.Execute(request);
        return Created(string.Empty, response);
    }
}