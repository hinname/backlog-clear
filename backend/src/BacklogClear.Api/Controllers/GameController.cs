using BacklogClear.Application.UseCases.Games.Delete;
using BacklogClear.Application.UseCases.Games.GetAll;
using BacklogClear.Application.UseCases.Games.GetById;
using BacklogClear.Application.UseCases.Games.Register;
using BacklogClear.Application.UseCases.Games.Update;
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
    public async Task<IActionResult> Register(
        [FromServices] IRegisterGameUseCase useCase,
        [FromBody] RequestGameJson request
    )
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseGamesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll([FromServices] IGetAllGamesUseCase useCase)
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
    public async Task<IActionResult> GetById(
        [FromServices] IGetGameByIdUseCase useCase,
        [FromRoute] long id
    )
    {
        var result = await useCase.Execute(id);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromServices] IDeleteGameUseCase useCase,
        [FromRoute] long id
    )
    {
        await useCase.Execute(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateGameUseCase useCase,
        [FromRoute] long id,
        [FromBody] RequestGameJson request
    )
    {
        await useCase.Execute(id, request);
        return NoContent();
    }
    
}