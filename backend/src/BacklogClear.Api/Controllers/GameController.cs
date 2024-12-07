using BacklogClear.Application.UseCases.Games.GetAll;
using BacklogClear.Application.UseCases.Games.GetById;
using BacklogClear.Application.UseCases.Games.Register;
using BacklogClear.Communication.Requests.Games;
using BacklogClear.Communication.Responses;
using BacklogClear.Communication.Responses.Games;
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

    [HttpGet]
    [ProducesResponseType(typeof(ResponseGamesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllGames([FromServices] IGetAllGamesUseCase useCase)
    {
        var result = await useCase.Execute();
        if (result.Games.Any())
            return Ok(result);
        return NoContent();
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseGameJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetGameById(
        [FromServices] IGetGameByIdUseCase useCase,
        [FromRoute] int id
    )
    {
        var result = await useCase.Execute(id);
        return Ok(result);
    }
}