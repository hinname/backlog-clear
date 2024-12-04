using BacklogClear.Application.UseCases.Games.Register;
using BacklogClear.Communication.Requests;
using BacklogClear.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BacklogClear.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController: ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredGameJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterGame(
        [FromServices] IRegisterGameUseCase useCase,
        [FromBody] RequestRegisterGameJson request
    )
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }
}